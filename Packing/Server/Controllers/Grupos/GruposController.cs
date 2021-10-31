using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Packing.Core.Productos;
using Packing.Negocio.Grupos;

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
    }
}
