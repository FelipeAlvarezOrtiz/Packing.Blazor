using System;
using MediatR;

namespace Packing.Shared.Pedidos
{
    public class ProcesarDetallesMasivoDto : IRequest
    {
        public Guid IdPedido { get; set; }
    }
}
