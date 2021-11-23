using MediatR;
using Microsoft.EntityFrameworkCore;
using Packing.Core.Productos;
using Packing.Persistencia;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Packing.Negocio.Productos
{
    public class ObtenerProductoPedido
    {
        public record Query : IRequest<Producto>
        {
            public int IdFormato { get; set; }
            public int IdPresentacion { get; set; }
            public int IdGrupo { get; set; }
        }

        public class Handler : IRequestHandler<Query, Producto>
        {
            private readonly ApplicationDbContext _context;

            public Handler(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Producto> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.Productos.Include(producto => producto.Presentacion)
                    .Include(x => x.Grupo).Include(x => x.Formato).Where(producto => 
                            (producto.Formato.IdFormato == request.IdFormato) && (producto.Grupo.IdGrupo == request.IdGrupo) 
                            && (producto.Presentacion.IdPresentacion == request.IdPresentacion))
                    .FirstOrDefaultAsync(cancellationToken);
            }
        }
    }
}
