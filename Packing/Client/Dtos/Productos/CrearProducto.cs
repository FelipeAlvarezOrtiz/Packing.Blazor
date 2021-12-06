using Packing.Core.Productos;
using System.ComponentModel.DataAnnotations;

namespace Packing.Client.Dtos.Productos
{
    public record FormatosDelGrupo
    {
        public int IdGrupo { get; set; }
    }

    public record PresentacionDelGrupo
    {
        public int IdGrupo { get; set; }
        public int IdFormato { get; set; }
    }
    
    public record RequestProductoPedido
    {
        public int IdFormato { get; set; }
        public int IdPresentacion { get; set; }
        public int IdGrupo { get; set; }
    }

    public record DetalleProductoEnCarro
    {
        public Producto ProductoIncorporado { get; set; }
        public int Cantidad { get; set; }
    }
    
}
