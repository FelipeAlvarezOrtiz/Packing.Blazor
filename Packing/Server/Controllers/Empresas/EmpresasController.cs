using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Packing.Core.Empresas;
using Packing.Negocio.Empresas;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Packing.Server.Controllers.Empresas
{
    public class EmpresasController : BaseController
    {
        [HttpGet("ObtenerEmpresas"),Authorize]
        public async Task<ActionResult<List<Empresa>>> ObtenerEmpresas()
        {
            return await Mediator.Send(new ListaEmpresas.Query { });
        }
    }
}
