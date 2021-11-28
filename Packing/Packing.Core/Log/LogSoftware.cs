using System;
using System.ComponentModel.DataAnnotations;

namespace Packing.Core.Log
{
    public class LogSoftware
    {
        [Key]
        public Guid IdLog { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
        [MaxLength(300)]
        public string Mensaje { get; set; }
        [MaxLength(100)]
        public string Modulo { get; set; }
    }
}
