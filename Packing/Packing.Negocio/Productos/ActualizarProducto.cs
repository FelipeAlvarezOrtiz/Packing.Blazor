using MediatR;
using Microsoft.EntityFrameworkCore;
using Packing.Negocio.Formatos;
using Packing.Negocio.Grupos;
using Packing.Negocio.Presentaciones;
using Packing.Persistencia;
using Packing.Shared.Productos;
using Packing.Shared.RespuestaEstandar;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Packing.Negocio.Productos
{
    public class ActualizarProducto
    {
        public class Handler : IRequestHandler<ActualizarProductoCommand,RespuestaContainer<int>>
        {
            private readonly ApplicationDbContext _context;
            private readonly IMediator _mediator;

            public Handler(ApplicationDbContext context, IMediator mediator)
            {
                _context = context;
                _mediator = mediator;
            }

            public async Task<RespuestaContainer<int>> Handle(ActualizarProductoCommand request, CancellationToken cancellationToken)
            {
                var productoEntidad = await _context.Productos.Where(x => x.IdProducto == request.IdProducto).FirstOrDefaultAsync(cancellationToken);
                if (productoEntidad is null) return GeneradorRespuestas<int>
                                                        .GenerarRespuestaIncorrecta(
                                                                            "El producto indicado no existe.",
                                                                            "Intento de actualización a producto inexistente", 
                                                                            "Packing.Negocio.ActualizaProducto", -1);
                var incFormato = await _mediator.Send(new ObtenerFormato.Command{ IdFormato = request.IdFormato }, cancellationToken);
                var incGrupo = await _mediator.Send(new ObtenerGrupo.Command { IdGrupo = request.IdGrupo }, cancellationToken);
                var incPresentacion = await _mediator.Send(new ObtenerPresentacion.Command { IdPresentacion = request.IdPresentacion }, cancellationToken);//ObtenerPresentacion(request.idPresentacion);
                var searchName = request.NombreProducto + " " + incFormato.NombreFormato + " " +
                                 incPresentacion.NombrePresentacion + " " + incGrupo.NombreGrupo;
                productoEntidad.Formato = incFormato;
                productoEntidad.Grupo = incGrupo;
                productoEntidad.Presentacion = incPresentacion;
                productoEntidad.NombreParaBusqueda = searchName;
                productoEntidad.NombreProducto = request.NombreProducto;
                productoEntidad.AfectoVencimiento = request.AfectoVencimiento;
                productoEntidad.MaxDiaVencimiento = request.DiaMaxVencimiento;
                productoEntidad.MinDiaVencimiento = request.DiaMinVencimiento;
                return await _context.SaveChangesAsync(cancellationToken) > 0
                    ? GeneradorRespuestas<int>.GenerarRespuestaCorrecta("Packing.Negocio.ActualizarProducto", 1)
                    : GeneradorRespuestas<int>.GenerarRespuestaIncorrecta("Ha ocurrido un error interno al actualizar el producto, intente más tarde",
                                                                            "ERROR AL SAVECHANGES","Packing.Negocio.ActualizaProducto",-1);
            }
        }
    }
}
