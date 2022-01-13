using MediatR;
using Packing.Core.Usuarios;

namespace Packing.Shared.Usuarios
{
    public class ObtenerUsuarioInternoPorId : IRequest<UsuarioInterno>
    {
        public int IdUsuario { get; set; }
    }
}
