using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Packing.Persistencia;
using Packing.Shared.Presentaciones;

namespace Packing.Negocio.Presentaciones
{
    public class ActualizarPresentacion : IRequestHandler<ActualizarPresentacionCommand>
    {
        private readonly ApplicationDbContext _context;

        public ActualizarPresentacion(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(ActualizarPresentacionCommand request, CancellationToken cancellationToken)
        {
            var presentacion = await _context.Presentaciones.FindAsync(request.IdPresentacion);
            if (presentacion is null) throw new Exception("Esa presentación no existe.");
            presentacion.NombrePresentacion = request.NombrePresentacion;
            _context.Presentaciones.Update(presentacion);
            return await _context.SaveChangesAsync(cancellationToken) > 0 
                ? Unit.Value 
                : throw new Exception("Ha ocurrido un error al guardar la información.");
        }
    }
}
