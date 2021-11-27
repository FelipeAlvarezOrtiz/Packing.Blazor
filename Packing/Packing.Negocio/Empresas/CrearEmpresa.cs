using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Packing.Core.Empresas;
using Packing.Core.Usuarios;
using Packing.Persistencia;
using Packing.Shared.EmpresaDto;

namespace Packing.Negocio.Empresas
{
    public class CrearEmpresa
    {
        public class Handler : IRequestHandler<CrearEmpresaCommand>
        {
            private readonly ApplicationDbContext _context;
            private readonly UserManager<AppUser> _userManager;
            private readonly RoleManager<IdentityRole> _roleManager;

            public Handler(ApplicationDbContext context, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
            {
                _context = context;
                _userManager = userManager;
                _roleManager = roleManager;
            }

            public async Task<Unit> Handle(CrearEmpresaCommand request, CancellationToken cancellationToken)
            {
                var resultUsuario = await _userManager.FindByEmailAsync(request.EmailUsuario);
                if (resultUsuario is not null) throw new Exception("Ese correo ya esta asociado a un usuario");
                var resultEmpresa = await _context.Empresas.Where(x => x.RutEmpresa.Equals(request.RutEmpresa.Replace(".", "")))
                    .FirstOrDefaultAsync(cancellationToken: cancellationToken);
                if (resultEmpresa is null)
                {
                    resultEmpresa = new Empresa()
                    {
                        RutEmpresa = request.RutEmpresa,
                        Direccion = request.Direccion,
                        NombreEmpresa = request.NombreEmpresa,
                        PersonaContacto = request.PersonaContacto,
                        RazonSocial = request.RazonSocial
                    };
                    await _context.Empresas.AddAsync(resultEmpresa, cancellationToken);
                    var resultGuardado = await _context.SaveChangesAsync(cancellationToken);
                    if (resultGuardado == 0) throw new Exception("Ha ocurrido un error al procesar la solicitud");
                }
                var nuevoUsuario = new AppUser()
                {
                    Empresa = resultEmpresa,
                    Direccion = request.Direccion,
                    Email = request.EmailUsuario,
                    UserName = request.EmailUsuario,
                    PhoneNumber = request.Telefono,
                    PhoneNumberConfirmed = true,
                    EmailConfirmed = true
                };
                var resultadoCreacion = await _userManager.CreateAsync(nuevoUsuario, "Packing01-");
                if (!resultadoCreacion.Succeeded)
                    throw new Exception(
                        $"Ha ocurrido un error al intentar crear al usuario con mensaje {resultadoCreacion.Errors.ToString()}");
                var resultUsuarioConsulta = await _userManager.FindByEmailAsync(nuevoUsuario.Email);
                if (resultUsuarioConsulta is not null)
                    await _userManager.AddToRoleAsync(resultUsuarioConsulta, request.RolUsuario);
                return Unit.Value;
            }

        }
    }
}
