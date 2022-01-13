using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Packing.Shared.Validador
{
    public class RutValidador : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is null)
            {
                return new ValidationResult("Rut no puede quedar vacio");
            }
            return RutValida(value.ToString()) ? ValidationResult.Success : new ValidationResult("Rut no valido");
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
