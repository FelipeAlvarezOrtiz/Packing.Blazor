using MediatR;

namespace Packing.Shared.EmpresaDto
{
    public record CrearEmpresaCommand : IRequest
    {
        public string NombreEmpresa { get; set; }
        public string RazonSocial { get; set; }
        public string RutEmpresa { get; set; }
        public string Direccion { get; set; }
        public string PersonaContacto { get; set; }
        public string NombreUsuario { get; set; }
        public string RolUsuario { get; set; }
        public string EmailUsuario { get; set; }
        public string Telefono { get; set; }
    }
}
