using MediatR;

namespace Packing.Shared.Usuarios
{
    public class ModificarEstadoUsuarioDto : IRequest
    {
        public int IdUsuarioInterno { get; set; }
    }
}
