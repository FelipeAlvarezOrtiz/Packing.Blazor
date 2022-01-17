using MediatR;
using Microsoft.EntityFrameworkCore;
using Packing.Core.Pedidos;
using Packing.Persistencia;
using Packing.Shared.Pedidos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Packing.Negocio.Mailing;
using Packing.Persistencia.Repositorios;

namespace Packing.Negocio.Pedidos
{
    public class CrearPedido : IRequestHandler<PedidoRequestDto>
    {
        private readonly ApplicationDbContext _context;
        private readonly EnviadorCorreos _enviadorCorreos;
        private readonly IConfiguration _configuration;

        public CrearPedido(IMediator mediator, ApplicationDbContext context, EnviadorCorreos enviadorCorreos, IConfiguration configuration)
        {
            _context = context;
            _enviadorCorreos = enviadorCorreos;
            _configuration = configuration;
        }

        public async Task<Unit> Handle(PedidoRequestDto request, CancellationToken cancellationToken)
        {
            if (request.ProductosEnPedido.Count <= 0)
                throw new Exception("No puedes ingresar un pedido sin productos.");
            var usuario = await _context.Usuarios.Where(user => user.Email.Equals(request.NombreUsuario))
                .Include(x => x.Empresa).FirstOrDefaultAsync(cancellationToken);
            if (usuario is null) throw new Exception("El usuario no existe.");

            var empresaMandante = await _context.Empresas.FindAsync(usuario.Empresa.IdEmpresa);
            
            if (empresaMandante is null)
                throw new Exception("Empresa no existe");
            var cabeceraPedido = Guid.NewGuid();
            var listaProductosEnPedido = new List<DetallePedido>();
            var pedidoCabecera = new Pedido()
            {
                GuidPedido = cabeceraPedido,
                EmpresaMandante = empresaMandante,
                FechaPedido = DateTime.Now,
                FechaUltimaActualizacion = DateTime.Now,
                Observacion = request.Observacion
            };
            var estado = await _context.EstadosPedidos.Where(x => x.IdEstadoPedido == 1).FirstOrDefaultAsync(cancellationToken);
            foreach (var detallePar in request.ProductosEnPedido)
            {
                var idProducto = detallePar.IdProducto;
                var productoInterno = await _context.Productos.Where(x => x.IdProducto == idProducto)
                                                                .Include(x => x.Grupo).Include(x => x.Presentacion)
                                                                .Include(x => x.Formato).FirstOrDefaultAsync(cancellationToken);
                
                var idDetalle = Guid.NewGuid();
                listaProductosEnPedido.Add(new DetallePedido()
                {
                    Cantidad = (uint)detallePar.Cantidad,
                    CantidadTotales = (uint)(detallePar.Cantidad * productoInterno.Formato.UnidadPorFormato),
                    Estado = estado,
                    FechaActualizacion = DateTime.Now,
                    ProductoInterno = productoInterno,
                    IdDetalle = idDetalle,
                    PedidoCabecera = pedidoCabecera
                });
            }
            pedidoCabecera.ProductosEnPedido = listaProductosEnPedido;
            await _context.Pedidos.AddAsync(pedidoCabecera, cancellationToken);

            var correoGerente = _configuration.GetValue<string>("EmailSender:CorreoGerente");
            
            _enviadorCorreos.EnviarEmail(correoGerente,
                "Nuevo pedido ingresado",
                ReemplazaTextoHtml(empresaMandante.NombreEmpresa,pedidoCabecera.GuidPedido.ToString()));
            return await _context.SaveChangesAsync(cancellationToken) > 0
                   ? Unit.Value
                   : throw new Exception(
                       "Ha ocurrido un error al ingresar el pedido, intente más tarde o contacte al administrador.");
        }

        public string ReemplazaTextoHtml(string empresa,string idPedido)
        {
            return $@"<h3>Han ingresado nuevo pedido: {idPedido}</h3>
                    <div>
                        <p>Te informamos que con fecha {DateTime.Now:dd-MM-yyyy} a las {DateTime.Now:HH:mm:ss}, la empresa {empresa} ha ingresado un nuevo pedido a la plataforma. </p>
                            <br /> <p>Revisa en pedidos recientes para encontrar más detalles.</p>
                    </div>";
        }
    }
}
