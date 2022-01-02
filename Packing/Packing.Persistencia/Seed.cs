using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Packing.Core.Pedidos;
using Packing.Core.Productos;
using Packing.Core.Usuarios;

namespace Packing.Persistencia
{
    public class Seed
    {
        public static async Task SeedData(ApplicationDbContext context)
        {
            if (!context.Formatos.Any())
            {
                var listaFormatos = new List<Formato>()
                {
                    new()
                    {
                        NombreFormato = "5x1",
                        UnidadPorFormato = 5
                    },
                    new()
                    {
                        NombreFormato = "6x1",
                        UnidadPorFormato = 6
                    },
                    new()
                    {
                        NombreFormato = "10x1",
                        UnidadPorFormato = 10
                    },
                    new()
                    {
                        NombreFormato = "12x1",
                        UnidadPorFormato = 12
                    },
                    new()
                    {
                        NombreFormato = "15x1",
                        UnidadPorFormato = 15
                    },
                };
                await context.Formatos.AddRangeAsync(listaFormatos);
            }
            if (!context.Presentaciones.Any())
            {
                var listaPresentaciones = new List<Presentacion>()
                {
                    new()
                    {
                        NombrePresentacion = "caja hortalicera"
                    },new()
                    {
                        NombrePresentacion = "caja frutera"
                    },new()
                    {
                        NombrePresentacion = "caja gris sin bolsa camisa"
                    },new()
                    {
                        NombrePresentacion = "caja gris con bolsa camisa"
                    }
                };
                await context.Presentaciones.AddRangeAsync(listaPresentaciones);
            }
            if (!context.Grupos.Any())
            {
                var listaGrupos = new List<GrupoProducto>()
                {
                    new ()
                    {
                        Imagen = "ALCACHOFA",
                        NombreGrupo = "Alcachofa"
                    },new ()
                    {
                        Imagen = "BROCOLI",
                        NombreGrupo = "Brocoli"
                    },new ()
                    {
                        Imagen = "CHIRIMOYA",
                        NombreGrupo = "Chorimoya"
                    },new ()
                    {
                        Imagen = "COLIFLOR",
                        NombreGrupo = "Coliflor"
                    },new ()
                    {
                        Imagen = "ESPINACA",
                        NombreGrupo = "Espinaca"
                    },new ()
                    {
                        Imagen = "LECHUGA ESCAROLA",
                        NombreGrupo = "Lechuga escarola"
                    },new ()
                    {
                        Imagen = "LIMON",
                        NombreGrupo = "Limón"
                    },new ()
                    {
                        Imagen = "PAPAS",
                        NombreGrupo = "Papa"
                    },new ()
                    {
                        Imagen = "PIMENTON",
                        NombreGrupo = "Pimentón"
                    },new ()
                    {
                        Imagen = "repollo crespo",
                        NombreGrupo = "Repollo crespo"
                    },new ()
                    {
                        Imagen = "TOMATE",
                        NombreGrupo = "Tomate"
                    },new ()
                    {
                        Imagen = "ZANAHORIA",
                        NombreGrupo = "Zanahoria"
                    },
                };
                await context.Grupos.AddRangeAsync(listaGrupos);
            }

            if (!context.EstadosPedidos.Any())
            {
                var listaEstados = new List<EstadoPedido>()
                {
                    new()
                    {
                        NombreEstado = "Creado"
                    },
                    new()
                    {
                        NombreEstado = "Procesando"
                    },
                    new()
                    {
                        NombreEstado = "Enviado"
                    },
                    new()
                    {
                        NombreEstado = "Procesado/Finalizado"
                    }
                };
                await context.EstadosPedidos.AddRangeAsync(listaEstados);
            }

            if (!context.CargosInternos.Any())
            {
                await context.CargosInternos.AddRangeAsync(new List<CargoInterno>()
                {
                    new()
                    {
                        NombreCargo = "Jefe de packing"
                    },
                    new ()
                    {
                        NombreCargo = "Cosechero"
                    }
                });
            }
            await context.SaveChangesAsync();
        }

        public static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!await roleManager.RoleExistsAsync("Administrador"))
            {
                await roleManager.CreateAsync(new IdentityRole("Administrador"));
                await roleManager.CreateAsync(new IdentityRole("Cliente"));
            }
        }

        public static async Task SeedUsuarios(UserManager<AppUser> userManager,ApplicationDbContext context)
        {
            var resultUsuarioAdmin = await userManager.FindByEmailAsync("me@felipealvarez.dev");
            var resultEmpresa = await context.Empresas.FindAsync(1);
            if (resultUsuarioAdmin is null)
            {
                var nuevoUsuarioAdmin = new AppUser()
                {
                    Empresa = resultEmpresa,
                    Direccion = "Población tocopilla segunda II pasaje 18 de septiembre #70",
                    Email = "me@felipealvarez.dev",
                    UserName = "me@felipealvarez.dev",
                    PhoneNumber = "+56944194679",
                    PhoneNumberConfirmed = true,
                    EmailConfirmed = true,
                    NombreUsuario = "Felipe Alvarez Admin",
                    RolUsuario = "Administrador"
                };
                var resultadoCreacion = await userManager.CreateAsync(nuevoUsuarioAdmin, "Felipe2-");
                if (resultadoCreacion.Succeeded)
                {
                    var resultUsuarioConsulta = await userManager.FindByEmailAsync("me@felipealvarez.dev");
                    await userManager.AddToRoleAsync(resultUsuarioConsulta, "Administrador");
                }
            }
            var resultUsuarioCliente = await userManager.FindByEmailAsync("cliente@felipealvarez.dev");
            if (resultUsuarioCliente is null)
            {
                var nuevoUsuarioCliente = new AppUser()
                {
                    Empresa = resultEmpresa,
                    Direccion = "Población tocopilla segunda II pasaje 18 de septiembre #70",
                    Email = "cliente@felipealvarez.dev",
                    UserName = "cliente@felipealvarez.dev",
                    PhoneNumber = "+56944194679",
                    PhoneNumberConfirmed = true,
                    EmailConfirmed = true,
                    NombreUsuario = "Felipe Alvarez Cliente",
                    RolUsuario = "Cliente"
                };
                var resultadoCreacion = await userManager.CreateAsync(nuevoUsuarioCliente, "Felipe2-");
                if (resultadoCreacion.Succeeded)
                {
                    var resultUsuarioConsulta = await userManager.FindByEmailAsync("cliente@felipealvarez.dev");
                    await userManager.AddToRoleAsync(resultUsuarioConsulta, "Cliente");
                }
            }
        }

    }
}
