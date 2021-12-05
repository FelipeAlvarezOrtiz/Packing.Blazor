using System;
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
    public class ObtenerInfoUsuario
    {
        public class Handler : IRequestHandler<ObtenerInfoUsuarioDto, AppUser>
        {
            private readonly ApplicationDbContext _context;

            public Handler(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<AppUser> Handle(ObtenerInfoUsuarioDto request, CancellationToken cancellationToken)
            {
                // OBTENER AL USUARIO SOLICITANDO --
                var usuarioSolicitante = await _context.Usuarios.Where(x => x.Email.Equals(request.UsuarioSolicitante))
                    .Include(x => x.Empresa)
                    .FirstOrDefaultAsync(cancellationToken);
                // VER SI ES ADMIN
                if (usuarioSolicitante is null) throw new Exception("El usuario solicitante no existe");
                // SI ES ADMIN DEVUELVE TODO LOS DATOS
                if (usuarioSolicitante.RolUsuario.Equals("Administrador"))
                    return await _context.Usuarios.Where(x => x.Email.Equals(request.EmailUsuarioBuscado))
                        .Include(x => x.Empresa)
                        .FirstOrDefaultAsync(cancellationToken);
                // SI ES NORMAL DEBE SER IGUAL AL SOLICITANDE Y DEVUELVE TODOS LOS DATOS
                if(request.UsuarioSolicitante.Equals(request.EmailUsuarioBuscado))
                    return await _context.Usuarios.Where(x => x.Email.Equals(request.UsuarioSolicitante))
                        .Include(x => x.Empresa)
                        .FirstOrDefaultAsync(cancellationToken);
                throw new Exception("El usuario no tiene permitido consultar por ese usuario.");
            }
        }
    }
}
