using System;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Packing.Core.Productos;

namespace Packing.Core.Pedidos
{
    public class DetallePedido
    {
        [Key, Required,BsonId, BsonGuidRepresentation(GuidRepresentation.Standard)]
        public Guid IdDetalle { get; set; }
        public Producto ProductoInterno { get; set; }
        public uint Cantidad { get; set; }
        public uint CantidadTotales { get; set; }
        public EstadoPedido Estado { get; set; }
        public DateTime FechaActualizacion { get; set; }
        [BsonIgnore]
        public virtual Pedido PedidoCabecera { get; set; }
    }
}
