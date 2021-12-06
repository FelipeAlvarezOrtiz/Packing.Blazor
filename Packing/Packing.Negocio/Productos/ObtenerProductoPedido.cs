using MediatR;
using Microsoft.EntityFrameworkCore;
using Packing.Core.Productos;
using Packing.Persistencia;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Packing.Shared.Pedidos;

namespace Packing.Negocio.Productos
{
    public class ObtenerProductoPedido
    {
        public class Handler : IRequestHandler<ObtenerProductoParaPedido, Producto>
        {
            private readonly ApplicationDbContext _context;

            public Handler(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Producto> Handle(ObtenerProductoParaPedido request, CancellationToken cancellationToken)
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
