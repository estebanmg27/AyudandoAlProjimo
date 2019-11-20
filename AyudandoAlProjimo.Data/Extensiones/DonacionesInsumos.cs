using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AyudandoAlProjimo.Data.Extensiones
{
    public class DonacionesInsumos
    {
        [Required(ErrorMessage = "El campo es obligatorio")]
        public string Insumo { get; set; }
        [Required(ErrorMessage = "El campo es obligatorio")]
        public int Cantidad { get; set; }
    }
}
