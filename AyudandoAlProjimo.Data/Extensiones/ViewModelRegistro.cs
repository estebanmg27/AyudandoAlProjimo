using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AyudandoAlProjimo.Data;

namespace AyudandoAlProjimo.Data.Extensiones
{
    public class ViewModelRegistro
    {
        [Required(ErrorMessage = "El Email es obligatorio")]
        [MaxLength(50, ErrorMessage = "50 caracteres como Maximo")]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }


        [MaxLength(20, ErrorMessage = "20 caracteres como Maximo")]
        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [RegularExpression(@"^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{8,}$",
        ErrorMessage = "La contraseña debe tener como mínimo una mayúscula, un número y 8 caracteres")]
        [Compare(("Password2"), ErrorMessage = "Las contraseñas deben ser iguales")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "La confirmación de la contraseña es obligatoria")]
        public string Password2 { get; set; }

        [Required(ErrorMessage = "La Fecha de Nacimiento es obligatoria")]
        [DataType(DataType.Date)]
        [EdadMinima(18)]
        public System.DateTime FechaNacimiento { get; set; }

        [MaxLength(30, ErrorMessage = "30 caracteres como máximo")]
        public string Token { get; set; }

        //public bool Activo { get; set; }
        //public System.DateTime FechaCracion { get; set; }
        //public int TipoUsuario { get; set; }
    }

    public class EdadMinima : ValidationAttribute
    {
        private int LimiteEdad;

        public EdadMinima(int LimiteEdad)
        {
            this.LimiteEdad = LimiteEdad;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            DateTime bday = DateTime.Parse(value.ToString());
            DateTime today = DateTime.Today;
            int age = today.Year - bday.Year;

            if (bday > today.AddYears(-age))
            {
                age--;
            }

            if (age < LimiteEdad)
            {
                var result = new ValidationResult("Solo se permite registrarse a personas mayores a 18 años");
                return result;
            }
            return null;
        }
    }
}

