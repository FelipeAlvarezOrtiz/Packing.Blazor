using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Packing.Core.Empresas;
using Packing.Persistencia;
using Packing.Shared.EmpresaDto;

namespace Packing.Negocio.Empresas
{
    public class ListaProductosEmpresa : IRequestHandler<ObtenerProductosDeEmpresa,List<DisponibilidadProducto>>
    {
        private readonly ApplicationDbContext _context;

        public ListaProductosEmpresa(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<DisponibilidadProducto>> Handle(ObtenerProductosDeEmpresa request, CancellationToken cancellationToken)
        {

            throw new NotImplementedException();
        }
    }
}
