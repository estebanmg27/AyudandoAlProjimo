using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AyudandoAlProjimo.Data
{
    public class DenunciasMetadata
    {
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public int IdMotivo { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string Comentarios { get; set; }
    }
}
