﻿using AyudandoAlProjimo.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AyudandoAlProjimo.Servicios
{
    public class PropuestaServicio
    {
        Entities ctx = new Entities();

        public int GenerarPropuestaGeneral(Propuestas p)
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

        public void NuevaPropuestaDonacionDeInsumos(Propuestas p, List<PropuestasDonacionesInsumos> insumos)
        {
            int PropuestaId = GenerarPropuestaGeneral(p);

            foreach (PropuestasDonacionesInsumos i in insumos)
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

            ctx.PropuestasValoraciones.Add(v);
            ctx.SaveChanges();

        }

        public List<Propuestas> Buscar(string busqueda)
        {
            List<Propuestas> lista = (from propuestas in ctx.Propuestas
                    join usuarios in ctx.Usuarios
                    on propuestas.IdUsuarioCreador equals usuarios.IdUsuario
                    where propuestas.Nombre.Contains(busqueda) ||
                    usuarios.UserName.Contains(busqueda) ||
                    usuarios.Nombre.Contains(busqueda) ||
                    usuarios.Apellido.Contains(busqueda)
                    orderby propuestas.FechaFin ascending
                    orderby propuestas.Valoracion descending
                    select propuestas).ToList();

            return lista;
        }

    }
}
