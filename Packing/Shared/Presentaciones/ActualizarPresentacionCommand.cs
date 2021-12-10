using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Packing.Shared.Presentaciones
{
    public record ActualizarPresentacionCommand : IRequest
    {
        [Required]
        public int IdPresentacion { get; set; }
        [Required,MaxLength(100),MinLength(5)]
        public string NombrePresentacion { get; set; }
    }
}
