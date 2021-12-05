using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Packing.Core.Empresas;

namespace Packing.Core.Usuarios
{
    public class AppUser : IdentityUser
    {
        [MaxLength(120)]
        public string Direccion { get; set; }
        [Display(Name = "Nombre de la empresa")]
        public Empresa Empresa { get; set; }
        [Required,MaxLength(50),MinLength(4)]
        public string RolUsuario { get; set; }
        [Required,MaxLength(150)]
        public string NombreUsuario { get; set; }
    }
}
