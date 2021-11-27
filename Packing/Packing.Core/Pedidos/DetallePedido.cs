using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Packing.Core.Productos;

namespace Packing.Core.Pedidos
{
    public class DetallePedido
    {
        [Key, Required]
        public Guid IdDetalle { get; set; }
        public Producto ProductoInterno { get; set; }
        public uint Cantidad { get; set; }
        public uint CantidadTotales { get; set; }
        public EstadoPedido Estado { get; set; }
        public DateTime FechaActualizacion { get; set; }
        public virtual Pedido PedidoCabecera { get; set; }
    }
}
