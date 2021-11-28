using MediatR;
using Packing.Core.Reportes;
using System;

namespace Packing.Shared.ReportesDto
{
    public class ReporteJefePackingQuery : IRequest<ReporteJefePacking>
    {
        public DateTime FechaDesde { get; set; }
        public DateTime FechaHasta { get; set; }
        public string UsuarioSolicitante { get; set; }
    }
}
