namespace Packing.Client.Dtos.Usuarios
{
    public record NuevoUsuarioInterno(string NombreUsuario, string RutUsuario, string NumeroTelefono, string CorreoUsuario, int IdCargoInterno);
    public record NuevoCargo(string NombreCargo);
    public record ActualizarUsuario(int IdUsuario, string NombreUsuario, string RutUsuario, string NumeroTelefono, string CorreoUsuario, int IdCargoInterno);
    public record ActualizarUsuarioInterno(int IdUsuario, string NombreUsuario, string NumeroTelefono, string CorreoUsuario, int IdCargoInterno);
    public record DeshabilitarUsuario(int IdUsuario);
    public record ExisteUsuario(string RutUsuario);
}
