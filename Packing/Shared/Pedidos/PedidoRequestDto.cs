using MediatR;
using System.Collections.Generic;

namespace Packing.Shared.Pedidos
{
    public class PedidoRequestDto : IRequest
    {
        public string NombreUsuario { get; set; }
        //id producto -- cantidad
        public List<DetallePedidoDto> ProductosEnPedido { get; set; }
        public string Observacion { get; set; }
    }

    public record DetallePedidoDto
    {
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
    }
}
