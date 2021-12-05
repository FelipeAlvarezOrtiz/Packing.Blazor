using System;
using System.Collections.Generic;
using MediatR;
using Packing.Core.Pedidos;

namespace Packing.Shared.Pedidos
{
    public class ObtenerPedidosPorEmpresaDto : IRequest<List<Pedido>>
    {
        public string NombreUsuario { get; set; }
        public DateTime? FechaDesde { get; set; }
        public DateTime? FechaHasta { get; set; }
    }
}

