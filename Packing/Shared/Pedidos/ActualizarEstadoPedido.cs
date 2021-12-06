using System;
using MediatR;

namespace Packing.Shared.Pedidos
{
    public record ActualizarEstadoPedido : IRequest<Unit>
    {
        public Guid IdPedido { get; set; }
        public Guid IdDetalle { get; set; }
        public int NuevoEstado { get; set; }
    }
}
