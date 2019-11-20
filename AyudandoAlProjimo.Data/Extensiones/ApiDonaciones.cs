using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AyudandoAlProjimo.Data.Extensiones
{
    public class ApiDonaciones
    {
        public int Estado { get; set; }
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public decimal Donacion { get; set; }
        public int TipoDonacion { get; set; }
        public int IdPropuesta { get; set; }
        public int Tipo { get; set; }
        public decimal TotalRecaudado { get; set; }
        public int IdPropuestaDIns { get; set; }
        public DateTime FechaDonacion { get; set; }
    }
}
