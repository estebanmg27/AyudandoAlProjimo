using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AyudandoAlProjimo.Data;
using AyudandoAlProjimo.Data.Extensiones;

namespace AyudandoAlProjimo.Servicios
{
    public class DonacionServicio
    {
        public List<ApiDonaciones> HistorialDonaciones(int id)
        {
            Entities ctx = new Entities();
            List<ApiDonaciones> donaciones = new List<ApiDonaciones>();

            var DonacionesMonetarias = (from p in ctx.Propuestas
                                        join prop_mon in ctx.PropuestasDonacionesMonetarias
                                         on p.IdPropuesta equals prop_mon.IdPropuesta
                                        join d_mon in ctx.DonacionesMonetarias
                                         on prop_mon.IdPropuestaDonacionMonetaria equals d_mon.IdPropuestaDonacionMonetaria
                                        where d_mon.IdUsuario == id
                                        select new ApiDonaciones
                                        {
                                            Estado = p.Estado,
                                            FechaDonacion = d_mon.FechaCreacion,
                                            IdUsuario = d_mon.IdUsuario,
                                            Donacion = d_mon.Dinero,
                                            Nombre = p.Nombre,
                                            Tipo = p.TipoDonacion,
                                            IdPropuesta = p.IdPropuesta
                                        }
                              ).ToList();

            var DonacionesInsumos = (from p in ctx.Propuestas
                                     join prop_in in ctx.PropuestasDonacionesInsumos
                                     on p.IdPropuesta equals prop_in.IdPropuesta
                                     join d_in in ctx.DonacionesInsumos
                                     on prop_in.IdPropuestaDonacionInsumo equals d_in.IdPropuestaDonacionInsumo
                                     where d_in.IdUsuario == id
                                     select new ApiDonaciones
                                     {
                                         Estado = p.Estado,
                                         IdUsuario = d_in.IdUsuario,
                                         Donacion = d_in.Cantidad,
                                         Nombre = p.Nombre,
                                         IdPropuestaDIns = prop_in.IdPropuestaDonacionInsumo,
                                         Tipo = p.TipoDonacion,
                                         IdPropuesta = p.IdPropuesta
                                     }
                             ).ToList();

            var DonacionesHoras = (from p in ctx.Propuestas
                                 join prop_hrs in ctx.PropuestasDonacionesHorasTrabajo
                                 on p.IdPropuesta equals prop_hrs.IdPropuesta
                                 join d_hrs in ctx.DonacionesHorasTrabajo
                                 on prop_hrs.IdPropuestaDonacionHorasTrabajo equals d_hrs.IdPropuestaDonacionHorasTrabajo
                                 where d_hrs.IdUsuario == id
                                 select new ApiDonaciones
                                 {
                                     Estado = p.Estado,
                              
                                     IdUsuario = d_hrs.IdUsuario,
                                     Donacion = d_hrs.Cantidad,
                                     Nombre = p.Nombre,
                                     Tipo = p.TipoDonacion,
                                     IdPropuesta = p.IdPropuesta
                                 }
                             ).ToList();

            donaciones.AddRange(DonacionesMonetarias);
            donaciones.AddRange(DonacionesInsumos);
            donaciones.AddRange(DonacionesHoras);

            return DonacionesHoras;
        }

        public List<ApiDonaciones> MisDonacionesId(int id)
        {
            throw new NotImplementedException();
        }
    }
}
