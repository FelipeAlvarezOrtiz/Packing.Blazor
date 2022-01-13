using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Packing.Shared.Usuarios
{
    public class CrearUsuarioInternoDto : IRequest
    {
        [Required(ErrorMessage = "El dato es requerido."), MinLength(5), MaxLength(50)]
        public string NombreUsuario { get; set; }
        [Required(ErrorMessage = "Este dato es requerido."),MaxLength(100),Phone(ErrorMessage = "No tiene formato válido de teléfono.")]
        public string NumeroTelefono { get; set; }
        [Required(ErrorMessage = "Debes ingresar el RUT de cliente. Sin puntos, con guión."), Packing.Shared.Validador.RutValidador]
        public string RutUsuario { get; set; }
        [Required(ErrorMessage = "El dato es requerido."),MaxLength(100),EmailAddress(ErrorMessage = "No tiene formato válido de correo.")]
        public string CorreoUsuario { get; set; }

        public bool UsuarioActivo { get; set; } = true;

        [Required(ErrorMessage = "El cargo es requerido."),Range(1,2,ErrorMessage = "Escoga una opción válida.")]
        public int IdCargo { get; set; }
    }
}
