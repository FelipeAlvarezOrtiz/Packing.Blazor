using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Packing.Shared.EmpresaDto
{
    public class ActualizarEmpresaCommand : IRequest
    {
        public int IdEmpresa { get; set; }
        [Required(ErrorMessage = "Este dato es requerido"), MinLength(5,ErrorMessage = "Muy pocos caracteres")]
        public string NombreEmpresa { get; set; }
        [Required(ErrorMessage = "Este dato es requerido"), MinLength(5, ErrorMessage = "Muy pocos caracteres")]
        public string RazonSocial { get; set; }
        [Required(ErrorMessage = "Este dato es requerido"), MinLength(9, ErrorMessage = "Muy pocos caracteres")]
        public string RutEmpresa { get; set; }
        [Required(ErrorMessage = "Este dato es requerido"), MinLength(5, ErrorMessage = "Muy pocos caracteres")]
        public string Direccion { get; set; }
        [Required(ErrorMessage = "Este dato es requerido"),MinLength(5, ErrorMessage = "Muy pocos caracteres")]
        public string PersonaContacto { get; set; }
    }
}
