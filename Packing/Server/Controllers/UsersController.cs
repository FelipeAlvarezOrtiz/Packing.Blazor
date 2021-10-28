using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Packing.Core.Usuarios;
using Packing.Persistencia;

namespace Packing.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsersController(ApplicationDbContext context, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [AllowAnonymous]
        [HttpGet("GenerarUsuario")]
        public async Task<ActionResult> GenerarUsuario()
        {
            await CrearRoles();
            var user = new AppUser
            {
                Email = "me@felipealvarez.dev",
                UserName = "me@felipealvarez.dev",
                Direccion = "Vicuña,Cuarta región",
                RolUsuario = "Administrador"
            };
            var result = await _userManager.CreateAsync(user, "Felipe2-");
            if (!result.Succeeded) return BadRequest("Ha ocurrido un problema al generar el usuario base");
            user = await _userManager.FindByEmailAsync("me@felipealvarez.dev");
            if (user is not null)
            {
                await _userManager.AddToRoleAsync(user, "Administrador");
            }
            return Ok();
        }

        private async Task CrearRoles()
        {
            string[] roles = { "Administrador", "Cliente" };
            foreach (var rol in roles)
            {
                if (!await _roleManager.RoleExistsAsync(rol))
                {
                    await _roleManager.CreateAsync(new IdentityRole(rol));
                }
            }
        }
    }
}
