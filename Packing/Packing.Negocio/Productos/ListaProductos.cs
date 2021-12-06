using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Packing.Core.Productos;
using Packing.Persistencia;

namespace Packing.Negocio.Productos
{
    public class ListaProductos
    {
        public record Query : IRequest<List<Producto>>{}

        public class Handler : IRequestHandler<Query, List<Producto>>
        {
            private readonly ApplicationDbContext _context;

            public Handler(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<List<Producto>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.Productos.Where(producto => producto.Disponible)
                    .Include(producto => producto.Formato)
                    .Include(producto => producto.Grupo).Include(producto => producto.Presentacion)
                    .ToListAsync(cancellationToken: cancellationToken);
            }
        }
    }
}
