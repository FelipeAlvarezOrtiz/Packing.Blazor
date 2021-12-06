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
    public class ActualizarEstadoDetalle : IRequestHandler<ActualizarEstadoPedido>
    {
        private readonly ApplicationDbContext _context;

        public ActualizarEstadoDetalle(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(ActualizarEstadoPedido request, CancellationToken cancellationToken)
        {
            var detalle = await _context.DetallePedidos
                .Where(detalle => detalle.PedidoCabecera.GuidPedido.Equals(request.IdPedido) && detalle.IdDetalle.Equals(request.IdDetalle))
                .Include(x => x.Estado)
                .FirstOrDefaultAsync(cancellationToken);
            if (detalle is null) throw new Exception("El detalle no existe");
            var nuevoEstado = await _context.EstadosPedidos.Where(x => x.IdEstadoPedido == request.NuevoEstado)
                .FirstOrDefaultAsync(cancellationToken);
            if (nuevoEstado is null) throw new Exception("Ese estado no existe");
            detalle.Estado = nuevoEstado;
            detalle.FechaActualizacion = DateTime.Now;
            _context.DetallePedidos.Update(detalle);
            return await _context.SaveChangesAsync(cancellationToken) > 0 
                ? Unit.Value 
                : throw new Exception("Ha ocurrido un problema al actualizar el detalle.");
        }
    }
}
