using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AyudandoAlProjimo.Data;

namespace AyudandoAlProjimo.Data.Extensiones
{
    public class ViewModelLogin
    {
        [Required(ErrorMessage = "El este campo  es obligatorio")]
        [EmailAddress(ErrorMessage = "Formato valido: ejemplo@unlam.com")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public int Valor { get; set; }
    }
}
