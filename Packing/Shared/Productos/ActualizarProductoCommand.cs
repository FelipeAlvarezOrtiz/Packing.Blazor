using System.ComponentModel.DataAnnotations;
using MediatR;
using Packing.Shared.RespuestaEstandar;

namespace Packing.Shared.Productos
{
    public record ActualizarProductoCommand : IRequest<RespuestaContainer<int>>
    {
        [Required(ErrorMessage = "El dato es necesario")]
        public int IdProducto { get; set; }
        [Required(ErrorMessage = "El dato es necesario"),MinLength(5,ErrorMessage = "El nombre no puede ser tan corto")]
        public string NombreProducto { get; set; }
        [Required(ErrorMessage = "El dato es necesario")]
        public int IdFormato { get; set; }
        [Required(ErrorMessage = "El dato es necesario")]
        public int IdGrupo { get; set; }
        [Required(ErrorMessage = "El dato es necesario")]
        public int IdPresentacion { get; set; }
        public bool AfectoVencimiento { get; set; }
        public int DiaMinVencimiento { get; set; }
        public int DiaMaxVencimiento { get; set; }
    }
}
