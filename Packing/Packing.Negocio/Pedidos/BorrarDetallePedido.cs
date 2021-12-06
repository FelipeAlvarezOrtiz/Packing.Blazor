using System;
using System.Linq;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Packing.Persistencia;
using Packing.Shared.Pedidos;

namespace Packing.Negocio.Pedidos
{
    public class BorrarDetallePedido : IRequestHandler<BorrarDetallePedidoDto>
    {
        private readonly ApplicationDbContext _context;

        public BorrarDetallePedido(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(BorrarDetallePedidoDto request, CancellationToken cancellationToken)
        {
            var detalle = await _context.DetallePedidos.Where(x => x.IdDetalle.Equals(request.IdDetalle))
                .FirstOrDefaultAsync(cancellationToken);
            if (detalle is null) throw new Exception("El detalle no existe.");
            _context.DetallePedidos.Remove(detalle);
            return await _context.SaveChangesAsync(cancellationToken) > 0 
                ? Unit.Value 
                : throw new Exception("Ha ocurrido un error al borrar el detalle.");
        }
    }
}
