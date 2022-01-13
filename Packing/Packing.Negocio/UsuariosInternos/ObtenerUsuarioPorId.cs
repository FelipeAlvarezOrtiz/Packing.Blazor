using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Packing.Core.Usuarios;
using Packing.Persistencia;
using Packing.Shared.Usuarios;

namespace Packing.Negocio.UsuariosInternos
{
    public class ObtenerUsuarioPorId : IRequestHandler<ObtenerUsuarioInternoPorId,UsuarioInterno>
    {
        private readonly ApplicationDbContext _context;

        public ObtenerUsuarioPorId(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<UsuarioInterno> Handle(ObtenerUsuarioInternoPorId request, CancellationToken cancellationToken)
        {
            return await _context.UsuariosInternos.Where(x => x.IdUsuario == request.IdUsuario)
                .Include(x => x.Cargo)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
