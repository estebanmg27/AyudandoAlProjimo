using AyudandoAlProjimo.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AyudandoAlProjimo.Servicios
{
    public class SesionServicio
    {
        public static Usuarios UsuarioSesion
        {
            get
            {
                return HttpContext.Current.Session["session"] as Usuarios;
            }
            set
            {
                HttpContext.Current.Session["session"] = value;
            }
        }     
    }
}
