using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Packing.Core.Empresas;
using Packing.Negocio.Empresas;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Packing.Shared.EmpresaDto;

namespace Packing.Server.Controllers.Empresas
{
    public class EmpresasController : BaseController
    {
        [HttpGet("ObtenerEmpresas"),Authorize]
        public async Task<ActionResult<List<Empresa>>> ObtenerEmpresas()
        {
            return await Mediator.Send(new ListaEmpresas.Query { });
        }

        [HttpGet("ObtenerEmpresa/{idEmpresa:int}")]
        public async Task<ActionResult<Empresa>> ObtenerEmpresa(int idEmpresa)
        {
            return await Mediator.Send(new ObtenerEmpresaQuery {IdEmpresa = idEmpresa});
        }


        [HttpDelete("DeshabilitarEmpresa/{idEmpresa:int}"),Authorize(Roles = "Administrador")]
        public async Task<ActionResult<Unit>> DeshabilitarEmpresa(int idEmpresa)
        {
            try
            {
                return await Mediator.Send(new DeshabilitarEmpresaCommand{IdEmpresa = idEmpresa });
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }

        [HttpPost("ActualizarEmpresa"), Authorize(Roles = "Administrador")]
        public async Task<ActionResult<Unit>> ActualizarEmpresa(ActualizarEmpresaCommand request)
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

        [HttpGet("ObtenerInfo/{rutEmpresa}")]
        public async Task<ActionResult<Empresa>> ObtenerInfo(string rutEmpresa)
        {
            return await Mediator.Send(new ObtenerEmpresaQueryPorRut{RutEmpresa = rutEmpresa});
        }
    }
}
