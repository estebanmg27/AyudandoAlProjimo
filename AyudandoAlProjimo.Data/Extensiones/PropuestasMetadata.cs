using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AyudandoAlProjimo.Data.Extensiones
{
    public class PropuestasMetadata
    {
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        public System.DateTime FechaFin { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string TelefonoContacto { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        public int TipoDonacion { get; set; }
    }
}
    