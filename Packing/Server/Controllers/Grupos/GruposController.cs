using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Packing.Core.Productos;
using Packing.Negocio.Grupos;
using Packing.Shared.GruposDto;

namespace Packing.Server.Controllers.Grupos
{
    [Authorize]
    public class GruposController : BaseController
    {
        [HttpGet("ObtenerGrupos")]
        public async Task<ActionResult<List<GrupoProducto>>> ObtenerGrupos()
        {
            return await Mediator.Send(new ListaGrupos.Query());
        }

        [HttpGet("ObtenerGruposProductos")]
        public async Task<ActionResult<List<GrupoProducto>>> ObtenerGruposProductos()
        {
            return await Mediator.Send(new ListaGruposProductos.Query());
        }

        [HttpPost("NuevoGrupo")]
        public async Task<ActionResult<Unit>> NuevoGrupo(CrearGrupoCommand request)
        {
            try
            {
                return await Mediator.Send(request);
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
                return BadRequest(error.Message);
            }
        }

        [HttpPost("ActualizarGrupo")]
        public async Task<ActionResult<Unit>> ActualizarGrupo(ActualizarGrupoCommand request)
        {
            return await Mediator.Send(request);
        }
    }
}
