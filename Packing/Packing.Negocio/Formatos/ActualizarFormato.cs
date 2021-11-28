using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Packing.Persistencia;
using Packing.Shared.FormatosDto;

namespace Packing.Negocio.Formatos
{
    public class ActualizarFormato
    {
        public class Handler : IRequestHandler<ActualizarFormatoCommand>
        {
            private readonly ApplicationDbContext _context;
            private readonly IMediator _mediator;

            public Handler(ApplicationDbContext context, IMediator mediator)
            {
                _context = context;
                _mediator = mediator;
            }

            public async Task<Unit> Handle(ActualizarFormatoCommand request, CancellationToken cancellationToken)
            {
                if(await _mediator.Send(new ObtenerFormato.Command{IdFormato = request.IdFormato}, cancellationToken) is null)
                    throw new Exception("No existe ese formato");
                var formato = await  _context.Formatos.FindAsync(request.IdFormato);
                
                formato.NombreFormato = request.NombreFormato;
                formato.UnidadPorFormato = request.CantidadPorFormato;

                return await _context.SaveChangesAsync(cancellationToken) > 0 ? Unit.Value : throw new Exception("Ha ocurrido un error al guardar el formato.");
            }
        }
    }
}
