using System.ComponentModel.DataAnnotations;

namespace Packing.Shared.EmpresaDto
{
    class ActualizarEmpresaCommand
    {
        [Required(ErrorMessage = "Este dato es requerido")]
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
        [Required(ErrorMessage = "Este dato es requerido"), MinLength(5, ErrorMessage = "Muy pocos caracteres")]
        public string NombreUsuario { get; set; }
        [Required(ErrorMessage = "Este dato es requerido"),MinLength(5,ErrorMessage = "Muy pocos caracteres")]
        public string RolUsuario { get; set; }
        [Required(ErrorMessage = "Este dato es requerido"),MinLength(5, ErrorMessage = "Muy pocos caracteres")]
        public string EmailUsuario { get; set; }
        [Required(ErrorMessage = "Este dato es requerido"),MinLength(5, ErrorMessage = "Muy pocos caracteres")]
        public string Telefono { get; set; }
    }
}
