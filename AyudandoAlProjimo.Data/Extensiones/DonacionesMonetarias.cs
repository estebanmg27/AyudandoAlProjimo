using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AyudandoAlProjimo.Data.Extensiones
{
    public class DonacionesMonetarias
    {
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public decimal Dinero { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string CBU { get; set; }
    }
}
