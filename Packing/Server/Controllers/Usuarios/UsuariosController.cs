using System;
using System.Collections.Generic;
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
        [HttpPost("ObtenerUsuario")]
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

        [HttpGet("ObtenerCargosInternos"),Authorize(Roles = "Administrador")]
        public async Task<ActionResult<List<CargoInterno>>> ObtenerCargosInternos()
        {
            return await Mediator.Send(new ObtenerCargosInternosQuery());
        }

        [HttpPost("CrearUsuarioInterno"),Authorize(Roles = "Administrador")]
        public async Task<ActionResult<Unit>> CrearUsuarioInterno(CrearUsuarioInternoDto request)
        {
            return await Mediator.Send(request);
        }

        [HttpGet("ObtenerUsuariosInternos"), Authorize(Roles = "Administrador")]
        public async Task<ActionResult<List<UsuarioInterno>>> ObtenerUsuariosInternos()
        {
            return await Mediator.Send(new ObtenerUsuariosInternosQuery());
        }
    }
}
