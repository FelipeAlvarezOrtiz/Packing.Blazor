using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Packing.Core.Productos;
using Packing.Persistencia;
using Packing.Shared.Productos;

namespace Packing.Negocio.Productos
{
    public class ObtenerProductoPorId
    {
        public class Handler : IRequestHandler<ObtenerProductoQuery, Producto>
        {
            private readonly ApplicationDbContext _context;

            public Handler(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Producto> Handle(ObtenerProductoQuery request, CancellationToken cancellationToken)
            {
                return await _context.Productos.Where(x => x.IdProducto==request.IdProducto)
                    .Include(x => x.Formato).Include(x => x.Grupo)
                    .Include(x => x.Presentacion).FirstOrDefaultAsync(cancellationToken);
            }
        }
    }
}
