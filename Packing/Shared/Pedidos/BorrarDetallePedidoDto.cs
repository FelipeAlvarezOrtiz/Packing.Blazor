using System;
using MediatR;

namespace Packing.Shared.Pedidos
{
    public record BorrarDetallePedidoDto : IRequest
    {
        public Guid IdDetalle { get; set; }
        public Guid IdCabecera { get; set; }
    }
}
