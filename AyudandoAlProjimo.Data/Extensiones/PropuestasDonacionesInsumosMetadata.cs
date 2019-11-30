using System.ComponentModel.DataAnnotations;

namespace AyudandoAlProjimo.Data.Extensiones
{
    public class PropuestasDonacionesInsumosMetadata
    {

        public int IdPropuestaDonacionInsumo { get; set; }
        public int IdPropuesta { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
		[MaxLength(50, ErrorMessage = "Maximo 50 caracteres")]
		public string Nombre { get; set; }

		[Required(ErrorMessage = "Este campo es olbigatorio")]
		public int Cantidad { get; set; }

    }
}
