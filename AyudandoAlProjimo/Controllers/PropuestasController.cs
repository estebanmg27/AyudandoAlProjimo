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
                    vista = "CrearPropuestaMoneraria";
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
            ViewBag.Nombre1 = form["Nombre1"];
            ViewBag.Telefono1 = form["Telefono1"];
            ViewBag.Nombre2 = form["Nombre2"];
            ViewBag.Telefono2 = form["Telefono2"];

            return View(vista, propuesta);

        }

        [HttpPost]
        public ActionResult CrearPropuesta(FormCollection form)
        {

            //if (propuestas.ObtenerMisPropuestas().Count < 3)
            //{
                return CrearNuevaPropuesta(form);
            //}
            //else
            //{
            //    ViewBag.MotivoError = "No puede crear más de tres propuestas";
            //    return View("../Shared/Error");
            //}

            
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
            Propuestas prop = RecuperarInformacion(form, new Propuestas());

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
                insumo.Cantidad = int.Parse(form["Cantidad[" + i + "]"]);
                insumos.Add(insumo);
            }

            return insumos;
        }

        public ActionResult VerDetallePropuesta(int id)
        {

            Propuestas p = propuestas.ObtenerPropuestaPorId(id);
            return View(p);
        }

        public ActionResult RealizarDonacion(int id)
        {
            Propuestas p = propuestas.ObtenerPropuestaPorId(id);
            if (p.TipoDonacion == 1)
            {
                return View("RealizarDonacionMonetaria", p);
            }
            else if (p.TipoDonacion == 2)
            {
                return View("RealizarDonacionDeInsumos", p);
            }
            else
            {
                return View("RealizarDonacionDeHorasDeTrabajo", p);
            }
        }

        [HttpPost]
        public ActionResult RealizarDonacionMonetaria(DonacionesMonetarias dm)
        {
            propuestas.AgregarDonacionMonetaria(dm);
            return Redirect("/Home/Index");
        }

        [HttpPost]
        public ActionResult RealizarDonacionDeInsumos(FormCollection form)
        {
            DonacionesInsumos di;
            List<DonacionesInsumos> insumos = new List<DonacionesInsumos>();

            for (int i = 0; i < Int32.Parse(form["Cantidad"]); i++)
            {
                di = new DonacionesInsumos();
                di.IdUsuario = Int32.Parse(form["IdUsuario"]);
                di.Cantidad = Int32.Parse(form["Cantidad[" + i + "]"]);
                di.IdPropuestaDonacionInsumo = Int32.Parse(form["IdPropuestaDonacionInsumo[" + i + "]"]);
                insumos.Add(di);
            }

            propuestas.AgregarDonacionDeInsumos(insumos);
            return Redirect("/Home/Index");
        }

        [HttpPost]
        public ActionResult RealizarDonacionDeHorasDeTrabajo(DonacionesHorasTrabajo dht)
        {
            propuestas.AgregarDonacionHorasDeTrabajo(dht);
            return Redirect("/Home/Index");
        }

        [HttpGet]
        public ActionResult CargarDenuncia(int id)
        {
            ViewBag.IdPropuesta = id;
            ViewBag.Motivos = propuestas.ObtenerMotivos();
            return View();
        }

        [HttpPost]
        public ActionResult CargarDenuncia(FormCollection form)
        {
            propuestas.AgregarDenuncia(form);
            return Redirect("/Home/Index");
        }


        [HttpPost]
        public ActionResult Calificar(FormCollection form)
        {
            int idPropuesta = Convert.ToInt32(form["IdPropuesta"]);
            propuestas.Valorar(form);
            return Redirect("/Propuestas/VerDetallePropuesta/" + idPropuesta);
        }

        [HttpPost]
        public ActionResult RealizarBusqueda(FormCollection form)
        {
            if (form["texto"] == null || form["texto"] == "")
            {
                return RedirectToAction("Propuestas");
            }
            else
            {
                string busqueda = form["texto"];
                List<Propuestas> ListaDePropuestas = propuestas.Buscar(busqueda);
                return View("Propuestas", ListaDePropuestas);
            }
        }

        public ActionResult Propuestas()
        {
            List<Propuestas> PropuestasLista = propuestas.ObtenerPropuestas();
            return View(PropuestasLista);
        }

        public ActionResult MisPropuestas()
        {
            List<Propuestas> propuestasPropias = propuestas.ObtenerMisPropuestas();
            return View(propuestasPropias);
        }
    }
}