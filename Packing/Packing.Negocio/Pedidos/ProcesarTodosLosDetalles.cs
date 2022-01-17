using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Packing.Persistencia;
using Packing.Shared.Pedidos;

namespace Packing.Negocio.Pedidos
{
    public class ProcesarTodosLosDetalles : IRequestHandler<ProcesarDetallesMasivoDto>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMediator _mediator;
        public ProcesarTodosLosDetalles(ApplicationDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(ProcesarDetallesMasivoDto request, CancellationToken cancellationToken)
        {
            //OBTENER TODOS LOS DETALLES
            var detalles = await _mediator.Send(new ObtenerDetallesDelPedidoDto {PedidoCabecera = request.IdPedido}, cancellationToken);
            if(detalles is null || detalles.Count == 0)
                throw new Exception("No existen detalles para ese pedido");
            var nuevoEstado = await _context.EstadosPedidos.Where(x => x.NombreEstado.Equals("Procesando"))
                .FirstOrDefaultAsync(cancellationToken);
            if (nuevoEstado is null)
                throw new Exception("La actualización de estado no se ha realizado, ya que el estado no existe.");
            foreach (var detalle in detalles.Where(detalle => detalle.Estado.IdEstadoPedido == 1))
            {
                detalle.Estado = nuevoEstado;
                detalle.FechaActualizacion = DateTime.Now;
            }
            _context.DetallePedidos.UpdateRange(detalles);
            return await _context.SaveChangesAsync(cancellationToken) > 0 
                ? Unit.Value 
                : throw new Exception("Error al guardar los datos.");
        }
    }
}
