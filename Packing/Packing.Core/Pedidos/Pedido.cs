using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Packing.Core.Empresas;

namespace Packing.Core.Pedidos
{
    public class Pedido
    {
        [Required, Key,BsonId,BsonGuidRepresentation(GuidRepresentation.Standard)]
        public Guid GuidPedido { get; set; }
        [Required]
        public Empresa EmpresaMandante { get; set; }
        public DateTime FechaPedido { get; set; }
        public DateTime FechaUltimaActualizacion { get; set; }
        [MaxLength(250, ErrorMessage = "Limite de carácteres sobrepasados.")]
        public string Observacion { get; set; }
        public List<DetallePedido> ProductosEnPedido { get; set; }
    }
}
