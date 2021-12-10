using System;
using System.Collections.Generic;
using MediatR;
using Packing.Core.Pedidos;

namespace Packing.Shared.ReportesDto
{
    public class BuscarPedidosReporteQuery : IRequest<List<Pedido>>
    {
        public int IdEmpresa { get; set; }
        public DateTime FechaDesde { get; set; }
        public DateTime FechaHasta { get; set; }
    }
}
