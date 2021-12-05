using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Packing.Shared.Presentaciones
{
    public class CrearPresentacionCommand : IRequest
    {
        [Required(ErrorMessage = "Este dato es necesario"),MinLength(4),MaxLength(50,ErrorMessage = "Maximo de caracteres alcanzado")]
        public string NombrePresentacion { get; set; }
    }
}
