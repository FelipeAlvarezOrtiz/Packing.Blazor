using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Packing.Core.Usuarios;
using Packing.Persistencia;

namespace Packing.Negocio.UsuariosInternos
{
    public class ObtenerUsuarioPorRut
    {
        public record Command : IRequest<UsuarioInterno>
        {
            public string RutUsuario { get; set; }
        }

        public class Handler : IRequestHandler<Command, UsuarioInterno>
        {
            private readonly ApplicationDbContext _context;

            public Handler(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<UsuarioInterno> Handle(Command request, CancellationToken cancellationToken)
            {
                return await _context.UsuariosInternos.Where(user => user.RutUsuario.Equals(request.RutUsuario))
                    .FirstOrDefaultAsync(cancellationToken);
            }
        }
    }
}
