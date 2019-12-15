using AyudandoAlProjimo.Data;
using AyudandoAlProjimo.Data.Enums;
using AyudandoAlProjimo.Data.Extensiones;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
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
                i.TelefonoContacto = p.TelefonoContacto;
                i.Descripcion = p.Descripcion;
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

        public  int TotalPropuestasActivas(int id)
        {
            return ctx.Propuestas.Where(x => x.Estado == 0 && x.IdUsuarioCreador == id).Count();
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
                    d.FechaCreacion = DateTime.Today;
                    ctx.DonacionesInsumos.Add(d);
                }
            }
            ctx.SaveChanges();
        }

        public void AgregarDonacionHorasDeTrabajo(DonacionesHorasTrabajo dht)
        {
            ctx.DonacionesHorasTrabajo.Add(dht);
            dht.FechaCreacion = DateTime.Today;
            ctx.SaveChanges();
        }

        public List<Propuestas> ObtenerPropuestas()
        {
            return ctx.Propuestas.ToList();
        }

        public void AgregarDenuncia(Denuncias d)
        {
            //Denuncias d = new Denuncias();
            //int Propuesta = Convert.ToInt32(form["IdPropuesta"]);
            //d.IdPropuesta = Propuesta;
            //d.Comentarios = form["Comentarios"];
            //d.IdMotivo = Convert.ToInt32(form["IdMotivo"]);
            //d.IdUsuario = Convert.ToInt32(form["IdUsuario"]);
            //d.Estado = 0; //pendiente de revisión
            //d.FechaCreacion = DateTime.Today;
            d.FechaCreacion = DateTime.Now;
            d.Estado = 0;
            d.IdUsuario = SesionServicio.UsuarioSesion.IdUsuario;
            ctx.Denuncias.Add(d);
            ctx.SaveChanges();
        }

        public Denuncias SoloDenunciarUnaVez(Denuncias d)
        {
            return ctx.Denuncias.Where(x => x.IdPropuesta == d.IdPropuesta && x.IdUsuario == SesionServicio.UsuarioSesion.IdUsuario).FirstOrDefault();

        }

        public List<MotivoDenuncia> ObtenerMotivos()
        {
            return ctx.MotivoDenuncia.ToList();
        }

        public void MeGusta(int Id)
        {
            PropuestasValoraciones v = new PropuestasValoraciones();
            int idUsuario = SesionServicio.UsuarioSesion.IdUsuario;
            var p = ObtenerPropuestaPorId(Id);

            var calificado = ctx.PropuestasValoraciones.Where(x => x.IdPropuesta == Id && idUsuario == x.IdUsuario).FirstOrDefault();
            if (calificado == null)
            {
                v.IdPropuesta = p.IdPropuesta;
                v.IdUsuario = idUsuario;
                v.Valoracion = true;
                ctx.PropuestasValoraciones.Add(v);
                ctx.SaveChanges();
            }
            else
            {
                calificado.Valoracion = true;
                ctx.SaveChanges();
            }
        }

        public void NoMeGusta(int Id)
        {
            PropuestasValoraciones v = new PropuestasValoraciones();
            int idUsuario = SesionServicio.UsuarioSesion.IdUsuario;
            var p = ObtenerPropuestaPorId(Id);

            var calificado = ctx.PropuestasValoraciones.Where(x => x.IdPropuesta == Id && idUsuario == x.IdUsuario).FirstOrDefault();
            if (calificado == null)
            {
                v.IdPropuesta = p.IdPropuesta;
                v.IdUsuario = idUsuario;
                v.Valoracion = false;
                ctx.PropuestasValoraciones.Add(v);
                ctx.SaveChanges();
            }
            else
            {
                calificado.Valoracion = false;
                ctx.SaveChanges();
            }
        }

        public decimal CalcularValoracionTotal(int Id)
        {
            var PropuestaActual = ObtenerPropuestaPorId(Id);
 
            var likes = ctx.PropuestasValoraciones.Where(x => x.IdPropuesta == Id && x.Valoracion == true).Count();
            var total = ctx.PropuestasValoraciones.Where(x => x.IdPropuesta == Id).Count();

            decimal Valoracion = (decimal)likes / total * 100; 
            decimal Resultado = Math.Round(Valoracion, 2);
            PropuestaActual.Valoracion = Resultado;
            ctx.SaveChanges();

            return Valoracion;
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

        public List<Propuestas> ObtenerPropuestasMenosLasPropias(Usuarios u)
        {
            List<Propuestas> Propuestas = (from p in ctx.Propuestas                                         
                                           where p.IdUsuarioCreador != u.IdUsuario
                                           select p).ToList();

            return Propuestas;
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
            List<Propuestas> misPropuestasActivas = (from propuestas in ctx.Propuestas
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

        public List<DonacionesMonetarias> ObtenerDonacionMonetariaId(int idPropuesta)
        {
            return ctx.PropuestasDonacionesMonetarias.Include("DonacionesMonetarias").FirstOrDefault(pdm => pdm.IdPropuesta == idPropuesta)
                ?.DonacionesMonetarias.ToList();
        }

        public int PuedeCrearPropuestas(int id)
        {
            var u = (from user in ctx.Usuarios
                     where user.IdUsuario == id
                     select user).First();

            if (u.Nombre == null || u.Nombre == "" || u.Apellido == null || u.Apellido == "" || u.FechaNacimiento == null || u.Foto == null || u.Foto == "")
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public void ModificarPropuesta(int id, Propuestas propuesta)
        {
            Propuestas pv = ObtenerPropuestaPorId(id);
            pv.Nombre = propuesta.Nombre;
            pv.Descripcion = propuesta.Descripcion;
            pv.FechaFin = propuesta.FechaFin;
            pv.TelefonoContacto = propuesta.TelefonoContacto;
            pv.TipoDonacion = propuesta.TipoDonacion;

            pv.Foto = !string.IsNullOrEmpty(propuesta.Foto) ? propuesta.Foto : string.Empty;
            switch (pv.TipoDonacion)
            {
                case 1:
                    pv.PropuestasDonacionesMonetarias.FirstOrDefault().Dinero = (propuesta as PropuestasDonacionesMonetarias).Dinero;
                    pv.PropuestasDonacionesMonetarias.FirstOrDefault().CBU = (propuesta as PropuestasDonacionesMonetarias).CBU;
                    break;
                case 2:
                   
                    break;
                case 3: 
                    pv.PropuestasDonacionesHorasTrabajo.FirstOrDefault().CantidadHoras = (propuesta as PropuestasDonacionesHorasTrabajo).CantidadHoras;
                    pv.PropuestasDonacionesHorasTrabajo.FirstOrDefault().Profesion = (propuesta as PropuestasDonacionesHorasTrabajo).Profesion;
                    break;
            }

            ctx.SaveChanges();
        }

        public void Modificar(Propuestas propuesta, List<PropuestasDonacionesInsumos> listaInsumos)
        {
            foreach (var i in listaInsumos)
            {
                PropuestasDonacionesInsumos insumo = propuesta.PropuestasDonacionesInsumos.Where(x => x.IdPropuestaDonacionInsumo == i.IdPropuestaDonacionInsumo).FirstOrDefault();


                if (insumo != null)
                {
                    insumo.Nombre = i.Nombre;
                    insumo.Cantidad = i.Cantidad;
                }
                else
                {
                    PropuestasDonacionesInsumos NuevoInsumo = new PropuestasDonacionesInsumos();
                    NuevoInsumo.Nombre = i.Nombre;
                    NuevoInsumo.Cantidad = i.Cantidad;
                    propuesta.PropuestasDonacionesInsumos.Add(NuevoInsumo);
                    ctx.SaveChanges();
                }
            }

            ctx.SaveChanges();
        }
    }

}

