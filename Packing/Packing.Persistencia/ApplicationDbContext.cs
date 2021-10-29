using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.Extensions.Options;
using Packing.Core.Empresas;
using Packing.Core.Notificaciones;
using Packing.Core.Pedidos;
using Packing.Core.Productos;
using Packing.Core.Usuarios;

namespace Packing.Persistencia
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions options, IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions) { }

        public DbSet<AppUser> Usuarios { get; set; }
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Presentacion> Presentaciones { get; set; }
        public DbSet<GrupoProducto> Grupos { get; set; }
        public DbSet<Formato> Formatos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<EstadoPedido> EstadosPedidos { get; set; }
        public DbSet<DetallePedido> DetallePedidos { get; set; }
        public DbSet<UsuarioInterno> UsuariosInternos { get; set; }
        public DbSet<CargoInterno> CargosInternos { get; set; }
        public DbSet<DisponibilidadProducto> Disponibilidades { get; set; }
        public DbSet<Notificacion> Notificaciones { get; set; }

    }
}
