using MediatR;

namespace Packing.Shared.Productos
{
    public record BorrarProductoCommand : IRequest
    {
        public int IdProducto { get; set; }
    }
}
