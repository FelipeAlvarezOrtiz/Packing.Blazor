using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Packing.Core.Productos;
using Packing.Persistencia;

namespace Packing.Negocio.Grupos
{
    public class ListaGrupos
    {
        public record Query : IRequest<List<GrupoProducto>> { }

        public class Handler : IRequestHandler<Query, List<GrupoProducto>>
        {
            private readonly ApplicationDbContext _context;

            public Handler(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<List<GrupoProducto>> Handle(Query request, CancellationToken cancellationToken)
            {
                //return await _context.Grupos.ToListAsync(cancellationToken: cancellationToken);
                return await _context.Productos.Include(x => x.Grupo)
                    .Where(x => x.Disponible)
                    .Select(x => x.Grupo).Distinct().ToListAsync(cancellationToken);
            }
        }
    }
}
