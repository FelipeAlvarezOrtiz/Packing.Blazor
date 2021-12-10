using MediatR;

namespace Packing.Shared.EmpresaDto
{
    public record DeshabilitarEmpresaCommand : IRequest
    {
        public int IdEmpresa { get; set; }
    }
}
