using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AyudandoAlProjimo.Data
{
    public class DonacionesInsumosMetadata
    { 
        [Required(ErrorMessage = "El campo es obligatorio")]
        public int Cantidad { get; set; }
    }
}
