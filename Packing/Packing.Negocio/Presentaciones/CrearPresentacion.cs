using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Packing.Core.Productos;
using Packing.Persistencia;

namespace Packing.Negocio.Presentaciones
{
    public class CrearPresentacion
    {
        public record Command : IRequest
        {
            public string NombrePresentacion { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly ApplicationDbContext _context;

            public Handler(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var nuevoNombre = request.NombrePresentacion;
                if (await ExistePresentacionPorNombre(nuevoNombre))
                {
                    throw new Exception("Esa presentación ya existe");
                }

                await _context.Presentaciones.AddAsync(new Presentacion()
                {
                    NombrePresentacion = nuevoNombre,
                }, cancellationToken);

                return await _context.SaveChangesAsync(cancellationToken) > 0
                    ? Unit.Value
                    : throw new Exception("Ha ocurrido un error interno, intentelo más tarde o contacte al dev.");
            }

            private async Task<bool> ExistePresentacionPorNombre(string nombrePresentacion)
            {
                var presentacionesExistentes = await _context.Presentaciones.ToListAsync();
                if (presentacionesExistentes.Count <= 0) return false;
                return await _context.Presentaciones
                    .Where(presentacion => presentacion.NombrePresentacion.Equals(nombrePresentacion))
                    .FirstOrDefaultAsync() is not null;
            }
        }
    }
}
