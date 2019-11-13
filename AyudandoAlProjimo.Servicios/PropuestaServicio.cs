using AyudandoAlProjimo.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AyudandoAlProjimo.Servicios
{
    public class PropuestaServicio
    {
        Entities ctx = new Entities();

        public int GenerarPropuestaGeneral(Propuestas p)
        {
            int IdUsuario = SesionServicio.UsuarioSesion.IdUsuario;
            Propuestas propuesta = new Propuestas();

            propuesta.Nombre = p.Nombre;
            propuesta.Descripcion = p.Descripcion;
            propuesta.TelefonoContacto = p.TelefonoContacto;
            propuesta.TipoDonacion = p.TipoDonacion;
            propuesta.FechaCreacion = DateTime.Today;
            propuesta.FechaFin = p.FechaFin;
            propuesta.Foto = p.Foto;
            propuesta.PropuestasReferencias = p.PropuestasReferencias;

            ctx.Propuestas.Add(propuesta);
            ctx.SaveChanges();

            return propuesta.IdPropuesta;

        }

        public void NuevaPropuestaDonacionMonetaria(PropuestasDonacionesMonetarias p)
        {
            int PropuestaId = GenerarPropuestaGeneral(p);
            p.IdPropuesta = PropuestaId;
            ctx.PropuestasDonacionesMonetarias.Add(p);
            ctx.SaveChanges();
        }

        public void NuevaPropuestaDonacionDeInsumos(PropuestasDonacionesInsumos p, List<PropuestasDonacionesInsumos> insumos)
        {
            int PropuestaId = GenerarPropuestaGeneral(p);

            foreach (PropuestasDonacionesInsumos i in insumos)
            {
                p.IdPropuesta = PropuestaId;
                ctx.PropuestasDonacionesInsumos.Add(i);
            }
            
            ctx.SaveChanges();
        }

        public void NuevaPropuestaDonacionHorasTrabajo(PropuestasDonacionesHorasTrabajo p)
        {
            int PropuestaId = GenerarPropuestaGeneral(p);
            p.IdPropuesta = PropuestaId;
            ctx.PropuestasDonacionesHorasTrabajo.Add(p);
            ctx.SaveChanges();
        }
    }
}
