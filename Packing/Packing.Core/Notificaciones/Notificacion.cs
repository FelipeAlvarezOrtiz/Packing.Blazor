using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Packing.Core.Notificaciones
{
    public class Notificacion
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GuidNotificacion { get; set; }
        [Range(1,3)]
        public int Severidad { get; set; }
        [MaxLength(200)]
        public string MensajeNotificacion { get; set; }
        public bool Notificado { get; set; } = false;
        [MaxLength(100)]
        public string OrigenNotificacion { get; set; }
    }
}
