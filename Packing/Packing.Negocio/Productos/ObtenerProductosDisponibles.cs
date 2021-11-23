using MediatR;
using Microsoft.EntityFrameworkCore;
using Packing.Core.Productos;
using Packing.Persistencia;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Packing.Negocio.Productos
{
    public class ObtenerProductosDisponibles
    {
        public record Query : IRequest<List<GrupoProducto>>
        {
            public int IdEmpresa { get; set; }
        }

        public class Handler : IRequestHandler<Query, List<GrupoProducto>>
        {
            private readonly ApplicationDbContext _context;

            public Handler(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<List<GrupoProducto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var empresa = await _context.Empresas.Where(e => e.IdEmpresa == request.IdEmpresa)
                                            .Include(x => x.ProductosVisibles).ThenInclude(p => p.Producto)
                                                .ThenInclude(p => p.Grupo).Distinct().FirstOrDefaultAsync(cancellationToken);
                return empresa.ProductosVisibles.Select(x => x.Producto.Grupo).Distinct().ToList();
            }
        }
    }
}
