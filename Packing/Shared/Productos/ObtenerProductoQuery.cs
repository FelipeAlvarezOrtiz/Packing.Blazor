using MediatR;
using Packing.Core.Productos;

namespace Packing.Shared.Productos
{
    public record ObtenerProductoQuery : IRequest<Producto>
    {
        public int IdProducto { get; set; }
    }
}
