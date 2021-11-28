using MediatR;

namespace Packing.Shared.GruposDto
{
    public record CrearGrupoCommand : IRequest
    {
        public string NombreGrupo { get; set; }
        public string FileName { get; set; }
        public byte[] ArchivoStream { get; set; }
    }
}
