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

namespace Packing.Negocio.Pedidos
{
    public class CrearPedido : IRequestHandler<PedidoRequestDto>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMediator _mediator;

        public CrearPedido(IMediator mediator, ApplicationDbContext context)
        {
            _mediator = mediator;
            _context = context;
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
            return await _context.SaveChangesAsync() > 0
                   ? Unit.Value
                   : throw new Exception(
                       "Ha ocurrido un error al ingresar el pedido, intente más tarde o contacte al administrador.");
        }
    }
}
