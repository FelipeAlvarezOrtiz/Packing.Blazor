using System.Collections.Generic;
using MediatR;
using Packing.Core.Usuarios;

namespace Packing.Shared.Usuarios
{
    public class ObtenerUsuariosInternosQuery : IRequest<List<UsuarioInterno>>
    {
    }
}
