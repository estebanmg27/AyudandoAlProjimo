using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AyudandoAlProjimo.Data.Extensiones
{
    public class UsuariosMetadata
    {

        public static ValidationResult ValidarMayoriaEdad(object value, ValidationContext context)
            => context.ObjectInstance is ViewModelRegistro usuario &&
            (default(DateTime) + (DateTime.Now - usuario.FechaNacimiento)).Year - 1 < 18 ? new ValidationResult("Solo se permite registrarse a personas mayores a 18 años") : ValidationResult.Success;

        public static ValidationResult ValidarEmailUnico(object value, ValidationContext context)
        {
            using (Entities ctx = new Entities())
            {
                var usuario = context.ObjectInstance as ViewModelRegistro;

                var existeEmail = ctx.Usuarios.Any(o => o.Email == usuario.Email);

                return existeEmail ? new ValidationResult($"Ya existe el Email, utilice otro") : ValidationResult.Success;
            }
        }      
    }
}
