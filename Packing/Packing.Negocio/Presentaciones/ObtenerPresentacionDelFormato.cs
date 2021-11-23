using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Packing.Core.Productos;
using Packing.Persistencia;

namespace Packing.Negocio.Presentaciones
{
    public class ObtenerPresentacionDelFormato
    {
        public record Query : IRequest<List<Presentacion>>
        {
            public int IdGrupo { get; set; }
            public int IdFormato { get; set; }
        }

        public class Handler : IRequestHandler<Query, List<Presentacion>>
        {
            private readonly ApplicationDbContext _context;

            public Handler(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<List<Presentacion>> Handle(Query request, CancellationToken cancellationToken)
            {
                var productosConEsaPresentacion = await _context.Productos.Include(producto => producto.Grupo)
                    .Include(producto => producto.Formato)
                    .Where(producto => producto.Grupo.IdGrupo == request.IdGrupo && producto.Formato.IdFormato == request.IdFormato)
                    .Select(producto => producto.Presentacion)
                    .Distinct()
                    .ToListAsync(cancellationToken: cancellationToken);
                return productosConEsaPresentacion;
            }
        }
    }
}
