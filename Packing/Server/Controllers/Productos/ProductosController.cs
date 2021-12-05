using Microsoft.AspNetCore.Mvc;
using Packing.Core.Productos;
using Packing.Negocio.Productos;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Packing.Shared.Productos;

namespace Packing.Server.Controllers.Productos
{
    [Authorize]
    public class ProductosController : BaseController
    {
        [HttpGet("ObtenerListaProductos")]
        public async Task<ActionResult<List<Producto>>> ObtenerProductos()
        {
            return await Mediator.Send(new ListaProductos.Query { });
        }

        [HttpPost("ObtenerProducto")]
        public async Task<ActionResult<Producto>> ObtenerProducto(ObtenerProductoPedido.Query request)
        {
            return await Mediator.Send(request);
        }

        [HttpPost("CrearProducto")]
        public async Task<ActionResult<Unit>> CrearProducto(CrearProductoCommand request)
        {
            return await Mediator.Send(request);
        }
    }
}
