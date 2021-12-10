using MediatR;
using Packing.Core.Reportes;
using System;

namespace Packing.Shared.ReportesDto
{
    public record ReporteCosecheroQuery : IRequest<ReporteCosechero>
    {
        public DateTime FechaDesde { get; set; }
        public DateTime FechaHasta { get; set; }
        public int IdEmpresa { get; set; }
        public string UsuarioSolicitante { get; set; }
    }
}
