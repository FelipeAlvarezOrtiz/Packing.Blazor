using MediatR;
using Microsoft.EntityFrameworkCore;
using Packing.Persistencia;
using Packing.Shared.Productos;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Packing.Negocio.Productos
{
    public class BorrarProducto
    {
        public class Handler : IRequestHandler<BorrarProductoCommand>
        {
            private readonly ApplicationDbContext _context;

            public Handler(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(BorrarProductoCommand request, CancellationToken cancellationToken)
            {
                var productoEntidad = await _context.Productos.Where(producto => producto.IdProducto == request.IdProducto).FirstOrDefaultAsync(cancellationToken);
                if (productoEntidad is null) throw new Exception("El producto solicitado ya no existe.");
                productoEntidad.Disponible = false;
                _context.Update(productoEntidad);
                return await _context.SaveChangesAsync(cancellationToken) > 0 
                    ? Unit.Value 
                    : throw new Exception("Ha ocurrido un error al eliminar el producto.");

            }
        }
    }
}
