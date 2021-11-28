using Packing.Core.Productos;
using System;
using System.Collections.Generic;

namespace Packing.Core.Reportes
{
    public class ReporteJefePacking
    {
        //Key de id empresa - Lista de Tuples <producto, cantidad> 
        public Dictionary<int, List<Tuple<Producto, int>>> Productos { get; set; }
        public DateTime FechaDeReporte { get; set; }
        public Tuple<DateTime, DateTime> RangoFechasConsideradas { get; set; }
        public string UsuarioSolicitante { get; set; }
    }
}
