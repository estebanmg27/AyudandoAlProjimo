using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AyudandoAlProjimo.Data.Extensiones
{
    public class DonacionesHorasTrabajo
    {
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public int CantidadHoras { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string Profesion { get; set; }
    }
}
