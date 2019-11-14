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
        public ActionResult CrearNuevaPropuesta(FormCollection form)
        {
            int donacion = Int32.Parse(form["TipoDonacion"]);
            Propuestas propuesta;
            string vista;

            switch (donacion)
            {
                case 1:
                    propuesta = new PropuestasDonacionesMonetarias();
                    vista = "CrearPropuestaMonetaria";
                    break;
                case 2:
                    propuesta = new PropuestasDonacionesInsumos();
                    vista = "CrearPropuestaDonacionInsumos";
                    break;
                default:
                    propuesta = new PropuestasDonacionesHorasTrabajo();
                    vista = "CrearPropuestaHorasTrabajo";
                    break;
            }

            propuesta = RecuperarInformacion(form, propuesta);
            return View(vista, propuesta);

        }

        [HttpPost]
        public ActionResult CrearPropuesta(FormCollection form)
        {
            return CrearNuevaPropuesta(form);
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

        [HttpPost]
        public ActionResult CrearPropuestaMoneraria(FormCollection form)
        {
            PropuestasDonacionesMonetarias prop = (PropuestasDonacionesMonetarias)RecuperarInformacion(form, new PropuestasDonacionesMonetarias());
            prop.Dinero = Decimal.Parse(form["Dinero"]);
            prop.CBU = form["CBU"];
            propuestas.NuevaPropuestaDonacionMonetaria(prop);
            return Redirect("/Home/Index");
        }

        [HttpPost]
        public ActionResult CrearPropuestaHorasTrabajo(FormCollection form)
        {
            PropuestasDonacionesHorasTrabajo prop = (PropuestasDonacionesHorasTrabajo)RecuperarInformacion(form, new PropuestasDonacionesHorasTrabajo());
            prop.CantidadHoras = Int32.Parse(form["CantidadHoras"]);
            prop.Profesion = form["Profesion"];
            propuestas.NuevaPropuestaDonacionHorasTrabajo(prop);
            return Redirect("/Home/Index");
        }

        public ActionResult CrearPropuestaDonacionInsumos(FormCollection form)
        {
            PropuestasDonacionesInsumos prop = (PropuestasDonacionesInsumos)RecuperarInformacion(form, new PropuestasDonacionesInsumos());

            List<PropuestasDonacionesInsumos> insumos = ListaDeInsumos(form);

            propuestas.NuevaPropuestaDonacionDeInsumos(prop, insumos);
            return Redirect("/Home/Index");
        }

        public List<PropuestasDonacionesInsumos> ListaDeInsumos(FormCollection form)
        {
            List<PropuestasDonacionesInsumos> insumos = new List<PropuestasDonacionesInsumos>();

            int CantidadInsumos = Int32.Parse(form["CantidadInsumos"]);

            for (int i = 0; i < CantidadInsumos; i++)
            {
                PropuestasDonacionesInsumos insumo = new PropuestasDonacionesInsumos();
                insumo.Nombre = form["Nombre"];
                insumo.Cantidad = Int32.Parse(form["Cantidad"]);
                insumos.Add(insumo);
            }

            return insumos;
        }
    }
}