using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Packing.Core.Pedidos;
using Packing.Shared.Pedidos;

namespace Packing.Server.Controllers.Pedidos
{
    public class PedidosController : BaseController
    {
        [HttpPost("ObtenerPedidosDeEmpresa")]
        public async Task<ActionResult<List<Pedido>>> ObtenerPedidosDeEmpresa(ObtenerPedidosPorEmpresaDto request)
        {
            return await Mediator.Send(request);
        }
    }
}
