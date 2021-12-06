using MediatR;
using Packing.Core.Productos;

namespace Packing.Shared.Pedidos
{
    public record ObtenerProductoParaPedido : IRequest<Producto>
    {
        public int IdFormato { get; set; }
        public int IdPresentacion { get; set; }
        public int IdGrupo { get; set; }
    }
}
