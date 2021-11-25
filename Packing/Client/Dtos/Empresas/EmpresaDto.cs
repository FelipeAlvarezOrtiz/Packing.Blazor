namespace Packing.Client.Dtos.Empresas
{
    public record CrearEmpresaDto(string NombreEmpresa, string RazonSocial, string RutEmpresa, string Direccion, string PersonaContacto, string NombreUsuario, string RolUsuario, string EmailUsuario, string Telefono);
    public record DisponibilidadEmpresa(int IdEmpresa,int IdProducto,int CantidadDisponible);
    public record ExisteEmpresaDto(string RutEmpresa);
    public record ActualizarEmpresa(string IdAppUser, string NombreEmpresa, string RazonSocial, string RutEmpresa, string Direccion, string PersonaContacto, string EmailUsuario, string Telefono);
}
