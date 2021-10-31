using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Packing.Core.Productos;
using Packing.Persistencia;

namespace Packing.Negocio.Formatos
{
    public class ObtenerFormatosDelGrupo
    {
        public record Query : IRequest<List<Formato>>
        {
            public int IdGrupo { get; set; }
        }

        public class Handler : IRequestHandler<Query, List<Formato>>
        {
            private readonly ApplicationDbContext _context;

            public Handler(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<List<Formato>> Handle(Query request, CancellationToken cancellationToken)
            {
                var productosConEseFormato = await _context.Productos.Include(producto => producto.Grupo)
                    .Where(producto => producto.Grupo.IdGrupo == request.IdGrupo).Select(producto => producto.Formato).ToListAsync(cancellationToken: cancellationToken);
                return productosConEseFormato;
            }
        }
    }
}
