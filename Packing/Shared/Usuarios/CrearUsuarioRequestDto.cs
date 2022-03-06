using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using MediatR;

namespace Packing.Shared.Usuarios
{
    public class CrearUsuarioRequestDto : IRequest
    {
        [Required(ErrorMessage = "Este dato es requerido"), RutValidador(ErrorMessage = "El R.U.T. no es válido")]
        public string RutEmpresa { get; set; }
        [Required(ErrorMessage = "Este dato es requerido")]
        public string NombreEmpresa { get; set; }
        [Required(ErrorMessage = "Este dato es requerido")]
        public string PersonaContacto { get; set; }
        [Required(ErrorMessage = "Este dato es requerido")]
        public string RazonSocial { get; set; }
        [Required(ErrorMessage = "Este dato es requerido")]
        public string Direccion { get; set; }
        [EmailAddress(ErrorMessage = "Formato de correo inválido")]
        public string EmailEmpresa { get; set; }
        [Required(ErrorMessage = "Este dato es requerido")]
        public string Telefono { get; set; }
        [Required(ErrorMessage = "Este dato es requerido")]
        public string NombreUsuario { get; set; }
        [EmailAddress(ErrorMessage = "Formato de correo inválido"),Required(ErrorMessage = "Este dato es requerido")]
        public string EmailUsuario { get; set; }
        [MaxLength(25),Required(ErrorMessage = "Este dato es requerido")]
        public string RolUsuario { get; set; }
    }

    public class RutValidador : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            if ((value is null))
            {
                return new ValidationResult("Rut no puede quedar vacio");
            }

            return RutValida(value.ToString()) ? ValidationResult.Success : new ValidationResult("El RUT no es valido.");
            //return value.ToString().Contains("-") ? ValidationResult.Success : new ValidationResult("Por favor revise el formato.");
        }
        private bool RutValida(string rut)
        {
            rut = rut.Replace(".", "").ToUpper();
            var expresion = new Regex("^([0-9]+-[0-9K])$");
            var dv = rut.Substring(rut.Length - 1, 1);
            if (!expresion.IsMatch(rut))
            {
                return false;
            }
            char[] charCorte = { '-' };
            var rutTemp = rut.Split(charCorte);
            return dv == Digito(int.Parse(rutTemp[0]));
        }

        private string Digito(int rut)
        {
            var suma = 0;
            var multiplicador = 1;
            while (rut != 0)
            {
                multiplicador++;
                if (multiplicador == 8)
                    multiplicador = 2;
                suma += (rut % 10) * multiplicador;
                rut = rut / 10;
            }
            suma = 11 - (suma % 11);
            return suma switch
            {
                11 => "0",
                10 => "K",
                _ => suma.ToString()
            };
        }
    }
}
