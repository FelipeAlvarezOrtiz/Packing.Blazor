using Packing.Core.Productos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Packing.Core.Reportes
{
    [NotMapped]
    public class ReporteCosechero
    {
        public Dictionary<int,Tuple<GrupoProducto,int>> Grupos { get; set; }
        public DateTime FechaDeReporte { get; set; }
        public Tuple<DateTime,DateTime> RangoFechasConsideradas { get; set; }
        public string UsuarioSolicitante { get; set; }
    }
}
