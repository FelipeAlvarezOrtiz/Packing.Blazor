using MediatR;
using System.Collections;
using System.Collections.Generic;

namespace Packing.Shared.Pedidos
{
    public class PedidoRequestDto : IRequest
    {
        public string NombreUsuario { get; set; }
        //id producto -- cantidad
        public ICollection ProductosEnPedido { get; set; } = new Dictionary<int, int>();
        public string Observacion { get; set; }
    }
}
