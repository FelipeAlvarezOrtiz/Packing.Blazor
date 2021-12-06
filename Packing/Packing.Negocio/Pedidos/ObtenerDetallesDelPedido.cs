using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Packing.Core.Pedidos;
using Packing.Persistencia;
using Packing.Shared.Pedidos;

namespace Packing.Negocio.Pedidos
{
    public class ObtenerDetallesDelPedido : IRequestHandler<ObtenerDetallesDelPedidoDto, List<DetallePedido>>
    {
        private readonly ApplicationDbContext _context;

        public ObtenerDetallesDelPedido(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<DetallePedido>> Handle(ObtenerDetallesDelPedidoDto request, CancellationToken cancellationToken)
        {
           return await _context.DetallePedidos
                .Where(x => x.PedidoCabecera.GuidPedido.Equals(request.PedidoCabecera))
                .Include(x => x.Estado)
                .Include(x => x.ProductoInterno)
                .ThenInclude(producto => producto.Grupo)
                .Include(x => x.ProductoInterno)
                .ThenInclude(producto => producto.Formato)
                .Include(x => x.ProductoInterno)
                .ThenInclude(producto => producto.Presentacion)
                .ToListAsync(cancellationToken);
        }
    }
}
