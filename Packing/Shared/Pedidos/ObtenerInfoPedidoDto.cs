using System;
using MediatR;
using Packing.Core.Pedidos;

namespace Packing.Shared.Pedidos
{
    public record ObtenerInfoPedidoDto : IRequest<Pedido>
    {
        public Guid GuidPedido { get; set; }
    }
}
