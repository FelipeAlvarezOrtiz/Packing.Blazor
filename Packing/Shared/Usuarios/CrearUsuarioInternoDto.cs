﻿using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Packing.Shared.Usuarios
{
    public class CrearUsuarioInternoDto : IRequest
    {
        [Required(ErrorMessage = "El dato es requerido."), MinLength(5), MaxLength(50)]
        public string NombreUsuario { get; set; }
        [Required(ErrorMessage = "Este dato es requerido."),MaxLength(100),Phone(ErrorMessage = "No tiene formato válido de teléfono.")]
        public string NumeroTelefono { get; set; }
        [Required(ErrorMessage = "El dato es requerido."), MinLength(9), MaxLength(15)]
        public string RutUsuario { get; set; }
        [Required(ErrorMessage = "El dato es requerido."),MaxLength(100),EmailAddress(ErrorMessage = "No tiene formato válido de correo.")]
        public string CorreoUsuario { get; set; }
        public bool UsuarioActivo { get; set; }
        [Required(ErrorMessage = "El cargo es requerido.")]
        public int IdCargo { get; set; }
    }
}