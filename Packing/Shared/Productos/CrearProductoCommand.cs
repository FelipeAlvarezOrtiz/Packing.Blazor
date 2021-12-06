using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Packing.Shared.Productos
{
    public record CrearProductoCommand : IRequest
    {
        [Required(ErrorMessage = "El dato es requerido"),MinLength(5, ErrorMessage = "El nombre es muy corto"), MaxLength(100, ErrorMessage = "El nombre es muy largo")]
        public string NombreProducto { get; set; }
        public string RutaImagen { get; set; }
        [Required(ErrorMessage = "El dato es requerido"),Range(1,9999,ErrorMessage = "Debe seleccionar una presentacion válida.")]
        public int IdPresentacion { get; set; }
        [Required(ErrorMessage = "El dato es requerido"),Range(1, 9999, ErrorMessage = "Debe seleccionar un formato válido.")]
        public int IdFormato { get; set; }
        [Required(ErrorMessage = "El dato es requerido"),Range(1, 9999, ErrorMessage = "Debe seleccionar un grupo válido.")]
        public int IdGrupo { get; set; }
        public bool AfectaVencimiento { get; set; }
        public int MinDiaVencimiento { get; set; }
        public int MaxDiaVencimiento { get; set; }
    }
}
