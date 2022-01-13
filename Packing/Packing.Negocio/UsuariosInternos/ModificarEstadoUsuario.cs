using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Packing.Persistencia;
using Packing.Shared.Usuarios;

namespace Packing.Negocio.UsuariosInternos
{
    public class ModificarEstadoUsuario : IRequestHandler<ModificarEstadoUsuarioDto>
    {
        private readonly ApplicationDbContext _context;

        public ModificarEstadoUsuario(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(ModificarEstadoUsuarioDto request, CancellationToken cancellationToken)
        {
            var usuarioInterno = await _context.UsuariosInternos.Where(x => x.IdUsuario == request.IdUsuarioInterno)
                .Include(x => x.Cargo).FirstOrDefaultAsync(cancellationToken);
            if (usuarioInterno is null)
                throw new Exception("El usuario interno no existe");
            usuarioInterno.UsuarioActivo = !usuarioInterno.UsuarioActivo;
            _context.UsuariosInternos.Update(usuarioInterno);
            return await _context.SaveChangesAsync(cancellationToken) > 0
                ? Unit.Value
                : throw new Exception("Ha ocurrido un error guardando los cambios.");
        }
    }
}
