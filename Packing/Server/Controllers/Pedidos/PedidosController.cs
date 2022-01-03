using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Packing.Core.Pedidos;
using Packing.Core.Reportes;
using Packing.Negocio.Pedidos.ObtenerEstados;
using Packing.Shared.Pedidos;
using Packing.Shared.ReportesDto;

namespace Packing.Server.Controllers.Pedidos
{
    [Authorize]
    public class PedidosController : BaseController
    {
        [HttpPost("ObtenerPedidosDeEmpresa")]
        public async Task<ActionResult<List<Pedido>>> ObtenerPedidosDeEmpresa(ObtenerPedidosPorEmpresaDto request)
        {
            return await Mediator.Send(request);
        }

        [HttpPost("IngresarPedido")]
        public async Task<ActionResult<Unit>> IngresarPedido(PedidoRequestDto request)
        {
            return await Mediator.Send(request);
        }

        [HttpPost("ObtenerDetalles")]
        public async Task<ActionResult<List<DetallePedido>>> ObtenerDetalles(ObtenerDetallesDelPedidoDto request)
        {
            return await Mediator.Send(request);
        }

        [HttpPut("ActualizarDetalle")]
        public async Task<ActionResult<Unit>> ActualizarDetalle(ActualizarEstadoPedido request)
        {
            return await Mediator.Send(request);
        }

        [HttpGet("ObtenerEstados")]
        public async Task<ActionResult<List<EstadoPedido>>> ObtenerEstados()
        {
            return await Mediator.Send(new ObtenerEstadosQuery());
        }

        [HttpPost("EliminarDetalle")]
        public async Task<ActionResult<Unit>> EliminarDetalle(BorrarDetallePedidoDto request)
        {
            return await Mediator.Send(request);
        }

        [HttpPost("ObtenerInfoPedido")]
        public async Task<ActionResult<Pedido>> ObtenerInfoPedido(ObtenerInfoPedidoDto requestPedido)
        {
            return await Mediator.Send(requestPedido);
        }

        [HttpPost("ObtenerPedidosParaReporte")]
        public async Task<ActionResult<List<Pedido>>> ObtenerPedidosParaReporte(BuscarPedidosReporteQuery request)
        {
            return await Mediator.Send(request);
        }

        [HttpPost("ObtenerReporteJefePacking")]
        public async Task<ActionResult<ReporteJefePacking>> ObtenerReporteJefePacking(ReporteJefePackingQuery request)
        {
            return await Mediator.Send(request);
        }

        [HttpPost("ObtenerReporteCosecheros")]
        public async Task<ActionResult<ReporteCosechero>> ObtenerReporteCosecheros(ReporteCosecheroQuery request)
        {
            return await Mediator.Send(request);
        }
    }
}
