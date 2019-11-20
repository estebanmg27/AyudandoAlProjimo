using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using AyudandoAlProjimo;
using AyudandoAlProjimo.Data.Extensiones;

namespace AyudandoAlProjimo.Data
{
    [MetadataType(typeof(PropuestasMetadata))]
    public partial class Propuestas
    {
        public string TipoDeDonacion()
        {
            if (this.TipoDonacion == 1)
            {
                return "monetaria";
            }
            else if (this.TipoDonacion == 2)
            {
                return "insumo";
            }
            else
            {
                return "horas de trabajo";
            }
        }
    }
}
