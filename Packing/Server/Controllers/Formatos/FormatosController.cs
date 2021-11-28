using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Packing.Core.Productos;
using Packing.Negocio.Formatos;
using Packing.Shared.FormatosDto;

namespace Packing.Server.Controllers.Formatos
{
    [Authorize]
    public class FormatosController : BaseController
    {
        [HttpGet("ObtenerFormatos")]
        public async Task<ActionResult<List<Formato>>> ObtenerFormatos()
        {
            return await Mediator.Send(new ListaFormatos.Query());
        }

        [HttpPost("Crear"),Authorize(Roles="Administrador")]
        public async Task<ActionResult<Unit>> CrearFormato(CrearFormatoCommand request)
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

        [HttpPost("ObtenerFormatosDisponibles")]
        public async Task<ActionResult<List<Formato>>> ObtenerFormatosDisponibles(ObtenerFormatosDelGrupo.Query request)
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
