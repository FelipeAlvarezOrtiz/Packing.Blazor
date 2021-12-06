using System;
using System.Collections.Generic;
using MediatR;
using Packing.Core.Pedidos;

namespace Packing.Shared.Pedidos
{
    public record ObtenerDetallesDelPedidoDto : IRequest<List<DetallePedido>>
    {
        public Guid PedidoCabecera { get; set; }
    }
}
