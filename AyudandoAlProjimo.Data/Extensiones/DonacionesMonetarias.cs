using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AyudandoAlProjimo.Data
{
    [MetadataType(typeof(DonacionesMonetariasMetadata))]
    public partial class DonacionesMonetarias
    {
        public string NombreSignificativoImagen
        {
            get
            {
               
                return string.Format("{0}", this.ArchivoTransferencia);
            }
        }

    }
}
