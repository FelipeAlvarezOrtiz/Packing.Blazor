using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Packing.Persistencia;
using Packing.Shared.GruposDto;

namespace Packing.Negocio.Grupos
{
    public class ActualizarGrupo : IRequestHandler<ActualizarGrupoCommand>
    {
        private readonly ApplicationDbContext _context;

        public ActualizarGrupo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(ActualizarGrupoCommand request, CancellationToken cancellationToken)
        {
            var grupo = await _context.Grupos.FindAsync(request.IdGrupo);
            if (grupo is null) throw new Exception("El grupo no existe.");
            grupo.NombreGrupo = request.NombreGrupo;
            _context.Grupos.Update(grupo);
            return await _context.SaveChangesAsync(cancellationToken) > 0 
                ? Unit.Value 
                : throw new Exception("No se ha podido actualizar el grupo.");
        }
    }
}
