using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Packing.Core.Usuarios;
using Packing.Negocio.Cargos;
using Packing.Persistencia;

namespace Packing.Negocio.UsuariosInternos
{
    public class CrearUsuarioInterno
    {
        public record Command : IRequest
        {
            public string NombreUsuario { get; set; }
            public string RutUsuario { get; set; }
            public string NumeroTelefono { get; set; }
            public string CorreoUsuario { get; set; }
            public int IdCargoInterno { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly ApplicationDbContext _context;
            private readonly IMediator _mediator;
            public Handler(ApplicationDbContext context, IMediator mediator)
            {
                _context = context;
                _mediator = mediator;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var usuarioInterno = await _mediator.Send(new ObtenerUsuarioPorRut.Command{RutUsuario = request.RutUsuario}, cancellationToken);
                if (usuarioInterno is not null) throw new Exception("Ese usuario ya está registrado en el sistema.");
                var cargo = await _mediator.Send(new ObtenerCargoPorId.Command{IdCargo = request.IdCargoInterno}, cancellationToken);
                if (cargo is null) throw new Exception("Ese cargo no existe.");
                usuarioInterno = new UsuarioInterno()
                {
                    RutUsuario = request.RutUsuario,
                    UsuarioActivo = true,
                    Cargo = cargo,
                    CorreoUsuario = request.CorreoUsuario,
                    NombreUsuario = request.NombreUsuario,
                    NumeroTelefono = request.NumeroTelefono
                };
                await _context.UsuariosInternos.AddAsync(usuarioInterno, cancellationToken);
                return await _context.SaveChangesAsync(cancellationToken) > 0 
                    ? Unit.Value 
                    : throw new Exception("Problema al guardar el usuario");
            }
        }
    }
}
