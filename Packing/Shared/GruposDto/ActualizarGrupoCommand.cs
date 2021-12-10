using MediatR;

namespace Packing.Shared.GruposDto
{
    public record ActualizarGrupoCommand : IRequest
    {
        public int IdGrupo { get; set; }
        public string NombreGrupo { get; set; }
    }
}
