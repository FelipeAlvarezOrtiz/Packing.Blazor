using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Packing.Core.Productos;
using Packing.Negocio.Presentaciones;
using Packing.Shared.Presentaciones;

namespace Packing.Server.Controllers.Presentaciones
{
    [Authorize]
    public class PresentacionesController : BaseController
    {
        [HttpGet("ObtenerPresentaciones")]
        public async Task<ActionResult<List<Presentacion>>> ObtenerPresentaciones()
        {
            return await Mediator.Send(new ListaPresentaciones.Query());
        }

        [HttpPost("CrearPresentacion")]
        public async Task<ActionResult<Unit>> CrearPresentacion(CrearPresentacion.Command request)
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

        [HttpPost("ObtenerPresentacionesDelGrupo")]
        public async Task<ActionResult<List<Presentacion>>> ObtenerPresentacionesDelGrupo(BuscarPresentacionesDelFormatoGrupo request)
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
