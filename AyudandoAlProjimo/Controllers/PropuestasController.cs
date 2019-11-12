using AyudandoAlProjimo.Data;
using AyudandoAlProjimo.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AyudandoAlProjimo.Controllers
{
    public class PropuestasController : Controller
    {
        PropuestaServicio propuestas = new PropuestaServicio();

        // GET: Propuestas
        public ActionResult CrearPropuesta()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CrearPropuesta(FormCollection form)
        {
            int donacion = Int32.Parse(form["TipoDonacion"]);
            Propuestas propuesta;
            string vista;

            switch (donacion)
            {
                case 1:
                    propuesta = new PropuestasDonacionesMonetarias();
                    vista = "CrearMonetaria";
                    break;
                case 2:
                    propuesta = new PropuestasDonacionesInsumos();
                    vista = "CrearInsumos";
                    break;
                default:
                    propuesta = new PropuestasDonacionesHorasTrabajo();
                    vista = "GenerarHorasTrabajo";
                    break;
            }

            propuesta = RecuperarInformacion(form, propuesta);
            return View(vista, propuesta);

        }

        public Propuestas RecuperarInformacion(FormCollection form, Propuestas p)
        {
            p.Nombre = form["Nombre"];
            p.Descripcion = form["Descripcion"];
            p.TelefonoContacto = form["TelefonoContacto"];
            p.TipoDonacion = Int32.Parse(form["TipoDonacion"]);
            p.FechaFin = System.DateTime.Parse(form["FechaFin"]);
            p.Foto = form["Foto"];

            PropuestasReferencias ref1 = new PropuestasReferencias();
            ref1.Nombre = form["Nombre1"];
            ref1.Telefono = form["Telefono1"];

            PropuestasReferencias ref2 = new PropuestasReferencias();
            ref2.Nombre = form["Nombre2"];
            ref2.Telefono = form["Telefono2"];

            p.PropuestasReferencias.Add(ref1);
            p.PropuestasReferencias.Add(ref2);

            return p;
        }
    }
}