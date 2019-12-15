using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AyudandoAlProjimo.Data
{
    public class DonacionesMonetariasMetadata
    {
        [Required(ErrorMessage = "Este campo es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "Solo se permiten valores positivos")]
        public decimal Dinero { get; set; }

        //[Required(ErrorMessage = "Este campo es obligatorio")]
        //public string ArchivoTransferencia { get; set; }




    }
}
