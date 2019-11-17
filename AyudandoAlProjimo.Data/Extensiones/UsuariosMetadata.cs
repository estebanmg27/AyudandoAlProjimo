using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AyudandoAlProjimo.Data;

namespace AyudandoAlProjimo.Data
{
    public class UsuariosMetadata
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
        //[Compare(nameof(password2), ErrorMessage ="Las contraseñas deben ser iguales")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        //[MaxLength(20, ErrorMessage = "20 caracteres como Maximo")]
        //[Required(ErrorMessage = "La contraseña es obligatoria")]
        //[RegularExpression(@"^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{8,}$",
        //ErrorMessage = "La contraseña debe tener como mínimo una mayúscula, un número y 8 caracteres")]
        //[DataType(DataType.Password)]
        //public string password2 { get; set; }

        [Required(ErrorMessage = "La Fecha de Nacimiento es obligatoria")]
        [DataType(DataType.Date)]
        [EdadMinima(18)]
        public System.DateTime FechaNacimiento { get; set; }

        [MaxLength(30, ErrorMessage = "30 caracteres como máximo")]
        public string Token { get; set; }

        //[Required(ErrorMessage = "El campo nombre es obligatorio")]
        //[MaxLength(50, ErrorMessage = "El nombre debe tener como máximo 50 caracteres.")]
        //public string Nombre { get; set; }

        //[Required(ErrorMessage = "El campo apellido es obligatorio")]
        //[MaxLength(50, ErrorMessage = "El apellido debe tener como máximo 50 caracteres.")]
        //public string Apellido { get; set; }

        //[Required(ErrorMessage = "El campo Foto es obligatorio")]
        //public string Foto { get; set; }

        //[Display(Name = "Nombre de usuario")]
        //public string UserName { get; set; }

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