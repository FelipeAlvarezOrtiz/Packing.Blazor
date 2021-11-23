using Packing.Core.Productos;
using System.ComponentModel.DataAnnotations;

namespace Packing.Client.Dtos.Productos
{
    public record CrearProducto
    {
        [MinLength(5,ErrorMessage = "El nombre es muy corto"),MaxLength(100,ErrorMessage = "El nombre es muy largo")]
        public string NombreProducto { get; set; }
        public string RutaImagen { get; set; }
        public int IdPresentacion { get; set; }
        public int IdFormato { get; set; }
        public int IdGrupo { get; set; }
        public bool AfectaVencimiento { get; set; }
        public int MinDiaVencimiento { get; set; }
        public int MaxDiaVencimiento { get; set; }
    }

    public record FormatosDelGrupo
    {
        public int IdGrupo { get; set; }
    }

    public record PresentacionDelGrupo
    {
        public int IdGrupo { get; set; }
        public int IdFormato { get; set; }
    }

    public record CrearFormato
    {
        public string NombreFormato { get; set; }
        public int CantidadPorFormato { get; set; }
    }

    public record CrearPresentacion
    {
        public string NombrePresentacion { get; set; }
    }

    public record CrearGrupo
    {
        public string NombreGrupo { get; set; }
        public string FileName { get; set; }
        public byte[] ArchivoStream { get; set; }
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
