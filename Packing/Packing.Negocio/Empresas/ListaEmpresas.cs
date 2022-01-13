using MediatR;
using Microsoft.EntityFrameworkCore;
using Packing.Core.Empresas;
using Packing.Persistencia;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Packing.Negocio.Empresas
{
    public class ListaEmpresas
    {
        public record Query : IRequest<List<Empresa>>{}

        public class Handler : IRequestHandler<Query, List<Empresa>>
        {
            private readonly ApplicationDbContext _context;

            public Handler(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<List<Empresa>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.Empresas.Include(x => x.ProductosVisibles)
                    .ThenInclude(p => p.Producto).ToListAsync(cancellationToken);
            }
        }
    }
}
