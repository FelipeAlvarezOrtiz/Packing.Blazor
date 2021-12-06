using Microsoft.AspNetCore.Mvc;
using Packing.Core.Productos;
using Packing.Negocio.Productos;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Packing.Shared.Pedidos;
using Packing.Shared.Productos;
using Packing.Shared.RespuestaEstandar;

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

        [HttpPost("ObtenerInfoProducto")]
        public async Task<ActionResult<Producto>> ObtenerProducto(ObtenerProductoParaPedido request)
        {
            return await Mediator.Send(request);
        }

        [HttpPost("ObtenerProducto")]
        public async Task<ActionResult<Producto>> ObtenerProducto(ObtenerProductoQuery request)
        {
            return await Mediator.Send(request);
        }

        [HttpPost("CrearProducto")]
        public async Task<ActionResult<Unit>> CrearProducto(CrearProductoCommand request)
        {
            return await Mediator.Send(request);
        }

        [HttpPut("ActualizarProducto")]
        public async Task<ActionResult<RespuestaContainer<int>>> ActualizarProducto(ActualizarProductoCommand request)
        {
            return await Mediator.Send(request);
        }

        [HttpDelete("BorrarProducto/{idProducto}")]
        public async Task<ActionResult<Unit>> BorrarProducto(int idProducto)
        {
            return await Mediator.Send(new BorrarProductoCommand{ IdProducto = idProducto });
        }
    }
}
