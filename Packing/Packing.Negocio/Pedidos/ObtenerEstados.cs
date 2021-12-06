using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Packing.Core.Pedidos;
using Packing.Persistencia;

namespace Packing.Negocio.Pedidos.ObtenerEstados
{
    public record ObtenerEstadosQuery : IRequest<List<EstadoPedido>>{}
    public class ObtenerEstados : IRequestHandler<ObtenerEstadosQuery, List<EstadoPedido>>
    {
        private readonly ApplicationDbContext _context;

        public ObtenerEstados(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<EstadoPedido>> Handle(ObtenerEstadosQuery request, CancellationToken cancellationToken)
        {
            return await _context.EstadosPedidos.ToListAsync(cancellationToken);
        }
    }
}
