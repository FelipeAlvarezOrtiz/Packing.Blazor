using MediatR;

namespace Packing.Shared.FormatosDto
{
    public record ActualizarFormatoCommand : IRequest
    {
        public int IdFormato { get; set; }
        public int CantidadPorFormato { get; set; }
        public string NombreFormato { get; set; }
    }
}
