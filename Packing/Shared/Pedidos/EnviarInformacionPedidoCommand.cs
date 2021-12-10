using System;
using MediatR;

namespace Packing.Shared.Pedidos
{
    public record EnviarInformacionPedidoCommand : IRequest
    {
        public DateTime FechaEnvio { get; set; }
        public string UsuarioEnviador { get; set; }
    }
}
