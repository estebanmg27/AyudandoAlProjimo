using AyudandoAlProjimo.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AyudandoAlProjimo.Servicios
{
    public class PropuestaServicio
    {
        Entities ctx = new Entities();

        private int GenerarPropuestaGeneral(Propuestas p)
        {
            int IdUsuario = SesionServicio.UsuarioSesion.IdUsuario;
            Propuestas propuesta = new Propuestas

            {
                Usuarios = ctx.Usuarios.Find(IdUsuario),
                Estado = 1,
                Nombre = p.Nombre,
                Descripcion = p.Descripcion,
                TelefonoContacto = p.TelefonoContacto,
                TipoDonacion = p.TipoDonacion,
                FechaCreacion = DateTime.Today,
                FechaFin = p.FechaFin,
                Foto = p.Foto,
                PropuestasReferencias = p.PropuestasReferencias,
            };

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

        public void NuevaPropuestaDonacionDeInsumos(Propuestas p, List<PropuestasDonacionesInsumos> ListaDeInsumos)
        {
                int PropuestaId = GenerarPropuestaGeneral(p);

                foreach (PropuestasDonacionesInsumos i in ListaDeInsumos)
                {
                    i.IdPropuesta = PropuestaId;
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

        public Propuestas ObtenerPropuestaPorId(int id)
        {
            return ctx.Propuestas.Find(id);
        }

        public void AgregarDonacionMonetaria(DonacionesMonetarias dm)
        {
            dm.FechaCreacion = DateTime.Today;
            ctx.DonacionesMonetarias.Add(dm);
            ctx.SaveChanges();
        }

        public void AgregarDonacionDeInsumos(List<DonacionesInsumos> di)
        {
            foreach (var d in di)
            {
                if (d.Cantidad > 0)
                {
                    ctx.DonacionesInsumos.Add(d);
                }
            }
            ctx.SaveChanges();
        }

        public void AgregarDonacionHorasDeTrabajo(DonacionesHorasTrabajo dht)
        {
            ctx.DonacionesHorasTrabajo.Add(dht);
            ctx.SaveChanges();
        }

        public List<Propuestas> ObtenerPropuestas()
        {
            return ctx.Propuestas.ToList();
        }

        public void AgregarDenuncia(FormCollection form)
        {
            Denuncias d = new Denuncias();
            int Propuesta = Convert.ToInt32(form["IdPropuesta"]);
            d.IdPropuesta = Propuesta;
            d.Comentarios = form["Comentarios"];
            d.IdMotivo = Convert.ToInt32(form["IdMotivo"]);
            d.IdUsuario = Convert.ToInt32(form["IdUsuario"]);
            d.Estado = 0;
            d.FechaCreacion = DateTime.Today;
            ctx.Denuncias.Add(d);
            ctx.SaveChanges();
        }

        public List<MotivoDenuncia> ObtenerMotivos()
        {
            return ctx.MotivoDenuncia.ToList();
        }

        public void Valorar(FormCollection form)
        {
            PropuestasValoraciones v = new PropuestasValoraciones();
            v.IdUsuario = Convert.ToInt32(form["IdUsuario"]);
            v.IdPropuesta = Convert.ToInt32(form["IdPropuesta"]);
            int calificado = NoCalificarMasDeUnaVez(v.IdPropuesta, v.IdUsuario);

            if (calificado == 0)
            {

                if (Convert.ToInt32(form["Valoracion"]) == 1)
                {
                    v.Valoracion = true;
                }
                else
                {
                    v.Valoracion = false;
                }

            }

            ctx.PropuestasValoraciones.Add(v);
            ctx.SaveChanges();
            
        }

        public decimal CalcularValoracionTotal(int Id)
        {
            var PropuestaActual = ObtenerPropuestaPorId(Id);
            
            var likes = ctx.PropuestasValoraciones.Where(x => x.IdPropuesta == Id && x.Valoracion == true).Count();
            var totalVotos = ctx.PropuestasValoraciones.Where(x => x.IdPropuesta == Id).Count();

            decimal Valoracion = (decimal)likes / totalVotos * 100;
            decimal Resultado = Math.Round(Valoracion, 2); 
            PropuestaActual.Valoracion = Resultado;
            ctx.SaveChanges();

            return Valoracion;
        }

        public int NoCalificarMasDeUnaVez(int IdUsuario, int IdPropuesta)
        {
            var calificacion = (from val in ctx.PropuestasValoraciones
                                where val.IdPropuesta == IdPropuesta &&
                                val.IdUsuario == IdUsuario
                                select val).FirstOrDefault();

            if (calificacion != null)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

       
        public List<Propuestas> Buscar(string busqueda)
        {
            List<Propuestas> lista = (from propuestas in ctx.Propuestas
                                      join usuarios in ctx.Usuarios
                                      on propuestas.IdUsuarioCreador equals usuarios.IdUsuario
                                      where propuestas.IdUsuarioCreador != SesionServicio.UsuarioSesion.IdUsuario
                                      where propuestas.Nombre.Contains(busqueda) ||
                                      usuarios.UserName.Contains(busqueda) ||
                                      usuarios.Nombre.Contains(busqueda) ||
                                      usuarios.Apellido.Contains(busqueda)
                                      orderby propuestas.FechaFin descending
                                      orderby propuestas.Valoracion descending
                                      select propuestas).ToList();

            return lista;
        }

        public List<Propuestas> ObtenerCincoPropuestasMasValoradas()
        {
            List<Propuestas> PropuestasMasValoradas = (from propuestas in ctx.Propuestas
                                                       join usuarios in ctx.Usuarios
                                                       on propuestas.IdUsuarioCreador equals usuarios.IdUsuario
                                                       where propuestas.Estado == 0
                                                       select propuestas).Take(5).ToList();

            return PropuestasMasValoradas;
        }

        public List<Propuestas> ObtenerMisPropuestas()
        {
            int IdUsuario = SesionServicio.UsuarioSesion.IdUsuario;
            List<Propuestas> misPropuestas = (from propuestas in ctx.Propuestas
                                              join user in ctx.Usuarios
                                              on propuestas.IdUsuarioCreador equals user.IdUsuario
                                              where propuestas.IdUsuarioCreador == IdUsuario
                                              select propuestas).ToList();

            return misPropuestas;
        }

        public List<Propuestas> ObtenerMisPropuestasActivas()
        {
            int IdUsuario = SesionServicio.UsuarioSesion.IdUsuario;
            List<Propuestas> misPropuestasActivas =    (from propuestas in ctx.Propuestas
                                                        join user in ctx.Usuarios
                                                        on propuestas.IdUsuarioCreador equals user.IdUsuario
                                                        where propuestas.IdUsuarioCreador == IdUsuario
                                                        where propuestas.Estado == 0
                                                        select propuestas).ToList();

            return misPropuestasActivas;
        }

        public List<Propuestas> ObtenerPropuestasActivas()
        {
            List<Propuestas> propuestasActivas = (from p in ctx.Propuestas
                                                  where p.Estado == 0
                                                  select p).ToList();

            return propuestasActivas;
        }

        public int CalcularTotalDonadoPropuestaInsunmos(int id)
        {
            int Total = 0;
            var lista = (from p in ctx.Propuestas
                         join p_insumos in ctx.PropuestasDonacionesInsumos
                         on p.IdPropuesta equals p_insumos.IdPropuesta
                         join d_insumos in ctx.DonacionesInsumos
                         on p_insumos.IdPropuestaDonacionInsumo equals d_insumos.IdPropuestaDonacionInsumo
                         where p_insumos.IdPropuestaDonacionInsumo == id
                         select d_insumos.Cantidad).ToList();


            foreach (int i in lista)
            {
                Total = Total + i;
            }
            return Total;

        }

        public int CalcularTotalDonadoPropuestaHoras(int id)
        {
            int Total = 0;
            var lista = (from p in ctx.Propuestas
                         join p_horas in ctx.PropuestasDonacionesHorasTrabajo
                         on p.IdPropuesta equals p_horas.IdPropuesta
                         join d_horas in ctx.DonacionesHorasTrabajo
                         on p_horas.IdPropuestaDonacionHorasTrabajo equals d_horas.IdPropuestaDonacionHorasTrabajo
                         where p.IdPropuesta == id
                         select d_horas.Cantidad).ToList();

            if (lista.Count > 0)
            {
                foreach (int i in lista)
                {
                    Total = Total + i;
                }
                return Total;
            }
            else return 0;
        }
    }

}
