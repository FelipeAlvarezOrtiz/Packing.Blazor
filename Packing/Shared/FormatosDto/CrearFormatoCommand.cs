using MediatR;

namespace Packing.Shared.FormatosDto
{
    public record CrearFormatoCommand : IRequest
    {
        public string NombreFormato { get; set; }
        public int CantidadPorFormato { get; set; }
    }
}
