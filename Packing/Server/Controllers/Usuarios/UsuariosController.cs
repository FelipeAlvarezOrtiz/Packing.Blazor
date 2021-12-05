using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Packing.Core.Usuarios;
using Packing.Shared.Usuarios;

namespace Packing.Server.Controllers.Usuarios
{
    [Authorize]
    public class UsuariosController : BaseController
    {
        [HttpGet("ObtenerUsuario")]
        public async Task<AppUser> ObtenerUsuario(ObtenerInfoUsuarioDto request)
        {
            return await Mediator.Send(request);
        }

        [HttpPost("CrearUsuario"),Authorize(Roles="Administrador")]
        public async Task<ActionResult<Unit>> CrearUsuario(CrearUsuarioRequestDto request)
        {
            try
            {
                return await Mediator.Send(request);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }


    }
}
