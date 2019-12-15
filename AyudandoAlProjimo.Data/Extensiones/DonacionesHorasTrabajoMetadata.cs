using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AyudandoAlProjimo.Data
{
    public class DonacionesHorasTrabajoMetadata
    {
        [Required(ErrorMessage = "Este campo es obligatorio")]
        //[Range(1, int.MaxValue, ErrorMessage = "Solo se permiten valores positivos")]
        public int CantidadHoras { get; set; }
    }
}
