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
    public class ActualizarUsuarioInterno : IRequestHandler<ActualizarUsuarioDto>
    {
        private readonly ApplicationDbContext _context;

        public ActualizarUsuarioInterno(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(ActualizarUsuarioDto request, CancellationToken cancellationToken)
        {
            var usuarioInterno = await _context.UsuariosInternos.Where(x => x.IdUsuario == request.IdUsuario)
                .Include(x => x.Cargo).FirstOrDefaultAsync(cancellationToken);
            if (usuarioInterno is null)
                throw new Exception("El usuario interno no existe.");
            var cargo = await _context.CargosInternos.Where(x => x.IdCargo == request.IdCargo)
                .FirstOrDefaultAsync(cancellationToken);
            usuarioInterno.Cargo = cargo;
            usuarioInterno.CorreoUsuario = request.CorreoUsuario;
            usuarioInterno.NombreUsuario = request.NombreUsuario;
            usuarioInterno.NumeroTelefono = request.NumeroTelefono;
            usuarioInterno.RutUsuario = request.RutUsuario;
            usuarioInterno.UsuarioActivo = request.UsuarioActivo;

            _context.UsuariosInternos.Update(usuarioInterno);

            return await _context.SaveChangesAsync(cancellationToken) > 0 
                ? Unit.Value 
                : throw new Exception("No se ha podido guardar los datos.");
        }
    }
}
