using System.ComponentModel.DataAnnotations;
using MediatR;
using Packing.Core.Usuarios;

namespace Packing.Shared.Usuarios
{
    public class ObtenerInfoUsuarioDto : IRequest<AppUser>
    {
        [Required]
        public string UsuarioSolicitante { get; set; }
        [Required]
        public string EmailUsuarioBuscado { get; set; }
    }
}
