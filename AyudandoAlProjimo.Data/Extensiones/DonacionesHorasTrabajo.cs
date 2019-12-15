using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AyudandoAlProjimo.Data
{
    [MetadataType(typeof(DonacionesHorasTrabajoMetadata))]
    public partial class DonacionesHorasTrabajo
    {
        public int CantidadHoras { get; set; }
    }
}
