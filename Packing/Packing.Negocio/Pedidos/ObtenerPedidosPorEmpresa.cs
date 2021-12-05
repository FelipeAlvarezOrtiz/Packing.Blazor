using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Packing.Core.Pedidos;
using Packing.Persistencia;
using Packing.Shared.Pedidos;

namespace Packing.Negocio.Pedidos
{
    public class ObtenerPedidosPorEmpresa
    {
        public class Handler : IRequestHandler<ObtenerPedidosPorEmpresaDto, List<Pedido>>
        {
            private ApplicationDbContext _context;
            public Handler(ApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<List<Pedido>> Handle(ObtenerPedidosPorEmpresaDto request, CancellationToken cancellationToken)
            {
                var usuario = await _context.Usuarios.Where(user => user.Email.Equals(request.NombreUsuario))
                    .Include(x => x.Empresa).FirstOrDefaultAsync(cancellationToken);
                if (usuario is null) throw new Exception("El usuario no existe.");
                if (usuario.RolUsuario.Equals("Administrador")) return await ObtenerPedidosAdmin(cancellationToken);
                if (request.FechaDesde is null || request.FechaHasta is null)
                    return await ObtenerPedidosSinFiltro(usuario.Empresa.IdEmpresa,cancellationToken);
                return await ObtenerPedidosConFiltro(usuario.Empresa.IdEmpresa,DateTime.Parse(request.FechaDesde.ToString()), 
                    DateTime.Parse(request.FechaHasta.ToString()),cancellationToken);
            }

            private async Task<List<Pedido>> ObtenerPedidosAdmin(CancellationToken cancellationToken)
            {
                return await _context.Pedidos.Include(x => x.EmpresaMandante)
                    .OrderByDescending(x => x.FechaPedido)
                    .Include(x => x.ProductosEnPedido)
                    .ThenInclude(x => x.Estado)
                    .Include(x => x.ProductosEnPedido)
                    .ThenInclude(x => x.ProductoInterno)
                    .Take(5)
                    .ToListAsync(cancellationToken);
            }

            private async Task<List<Pedido>> ObtenerPedidosSinFiltro(int idEmpresa,CancellationToken cancellationToken)
            {
                return await _context.Pedidos.Include(x => x.EmpresaMandante)
                    .Where(x => x.EmpresaMandante.IdEmpresa == idEmpresa).OrderByDescending(x => x.FechaPedido)
                    .Include(x=> x.ProductosEnPedido)
                    .ThenInclude(x=> x.Estado)
                    .Include(x=> x.ProductosEnPedido)
                    .ThenInclude(x => x.ProductoInterno)
                    .Take(5)
                    .ToListAsync(cancellationToken);
            }

            private async Task<List<Pedido>> ObtenerPedidosConFiltro(int idEmpresa, DateTime fechaDesde, DateTime fechaHasta, CancellationToken cancellationToken)
            {
                return await _context.Pedidos.Include(x => x.EmpresaMandante)
                    .Where(x => x.EmpresaMandante.IdEmpresa == idEmpresa).OrderByDescending(x => x.FechaPedido)
                    .Where(x => (DateTime.Compare(x.FechaPedido,fechaDesde) >= 0) && (DateTime.Compare(x.FechaPedido,fechaHasta) <= 0))
                    .Include(x => x.ProductosEnPedido)
                    .ThenInclude(x => x.Estado)
                    .Include(x => x.ProductosEnPedido)
                    .ThenInclude(x => x.ProductoInterno)
                    //.LeftJoin()
                    .ToListAsync(cancellationToken);
            }

            private async Task<List<Pedido>> ObtenerPedidosConFiltro()
            {
                return null;
            }
        }
    }
}
