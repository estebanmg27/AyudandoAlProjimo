using AyudandoAlProjimo.Data;
using AyudandoAlProjimo.Data.Extensiones;
using AyudandoAlProjimo.Servicios;
using AyudandoAlProjimo.Utilities;
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
        UsuarioServicio usuarios = new UsuarioServicio();
        SesionServicio sesion = new SesionServicio();

        // GET: Propuestas
        public ActionResult CrearPropuesta()
        {
            if (propuestas.TotalPropuestasActivas() == 3)
            {
                return RedirectToAction("MisPropuestas", "Propuestas");
            }
            if (SesionServicio.UsuarioSesion.UserName == null)
            {
                TempData["SinPerfil"] = "Para crear una propuesta primero es necesario que completes tu perfil";
                return Redirect("/Usuario/MiPerfil");
            }
            else
            {
                return View();
            }
        }


        [HttpPost]
        public ActionResult CrearNuevaPropuesta(FormCollection form)
        {
            int donacion = Int32.Parse(form["TipoDonacion"]);
            Propuestas propuesta;
            string vista;


            if (!ModelState.IsValid)
            {
                return View("CrearPropuesta");
            }
            else
            {
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
        }

        [HttpPost]
        public ActionResult CrearPropuesta(FormCollection form)
        {
            if (ModelState.IsValid)
            {
                return CrearNuevaPropuesta(form);
            }
            else
            {
                return View("CrearPropuesta");
            }
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
            if (ModelState.IsValid)
            {
                PropuestasDonacionesMonetarias prop = (PropuestasDonacionesMonetarias)RecuperarInformacion(form, new PropuestasDonacionesMonetarias());
                prop.Dinero = Decimal.Parse(form["Dinero"]);
                prop.CBU = form["CBU"];
                propuestas.NuevaPropuestaDonacionMonetaria(prop);
                return Redirect("/Home/Index");
            }
            else
            {
                return View("CrearPropuestaMoneraria");
            }
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
            if (ModelState.IsValid)
            {
                Propuestas prop = RecuperarInformacion(form, new Propuestas());

                List<PropuestasDonacionesInsumos> insumos = ListaDeInsumos(form);

                propuestas.NuevaPropuestaDonacionDeInsumos(prop, insumos);
                return Redirect("/Home/Index");
            }
            else
            {
                return View("CrearPropuestaDonacionInsumos");
            }
        }

        private List<PropuestasDonacionesInsumos> ListaDeInsumos(FormCollection form)
        {
            List<PropuestasDonacionesInsumos> insumos = new List<PropuestasDonacionesInsumos>();

            int CantidadInsumos = Int32.Parse(form["CantidadInsumos"]);
            PropuestasDonacionesInsumos insumo;

            for (int i = 0; i < CantidadInsumos; i++)
            {
                insumo = new PropuestasDonacionesInsumos();
                insumo.Nombre = form["Nombres[" + i + "]"];
                insumo.Cantidad = Int32.Parse(form["Cantidad[" + i + "]"]);
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
            Usuarios user = SesionServicio.UsuarioSesion;
            ViewBag.IdDonante = user.IdUsuario;

            if (user != null && user.IdUsuario != p.IdUsuarioCreador)
            { 
                switch (p.TipoDonacion)
                {
                    case 1:
                        return View("DonacionMonetaria", p);

                    case 2:
                        return View("DonacionInsumos", p);

                    case 3:
                        return View("DonacionHorasTrabajo", p);
                }
            }

            return Redirect("/Home/Index");
        }

        [HttpPost]
        public ActionResult RealizarDonacionMonetaria(DonacionesMonetarias dm)
        {
            if (Request.Files.Count > 0 && Request.Files[0].ContentLength > 0)
            {
                string nombresignificativo = dm.ArchivoTransferencia + DateTime.Now.ToString();
                string pathRelativoImagen = ImagenesUtility.Guardar(Request.Files[0], nombresignificativo);
                dm.ArchivoTransferencia = pathRelativoImagen;
            }

            propuestas.AgregarDonacionMonetaria(dm);
            return Redirect("/Home/Index");
        }

        [HttpPost]
        public ActionResult RealizarDonacionDeInsumos(FormCollection form)
        {
            if (ModelState.IsValid)
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
            else
            {
                return View(form);
            }
        }

        [HttpPost]
        public ActionResult RealizarDonacionDeHorasDeTrabajo(DonacionesHorasTrabajo dh)
        {
            propuestas.AgregarDonacionHorasDeTrabajo(dh);
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
        public ActionResult CargarDenuncia(Denuncias d)
        {
            if (!ModelState.IsValid)
            {
                return View(d);
            }
            if (propuestas.SoloDenunciarUnaVez(d) == null)
            {
                propuestas.AgregarDenuncia(d);
                TempData["DenunciaRealizada"] = "Denuncia realizada";
                return Redirect("/Home/Index");
            }
            else
            {
                TempData["DenunciaRepetida"] = "Ya denunciaste esta propuesta";
                return Redirect("/Home/Index");
            }
        }


        [HttpPost]
        public ActionResult Calificar(FormCollection form, int id)
        {
            int idPropuesta = Convert.ToInt32(form["IdPropuesta"]);
            propuestas.Valorar(form);
            propuestas.CalcularValoracionTotal(id);
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

            var contador = 0;

            foreach (var propuesta in propuestasPropias)
            {
                if (propuesta.Estado == 0)
                {
                    contador++;
                }
            }

            ViewBag.propuestasCreadas = contador;
            return View(propuestasPropias);
        }

        [HttpGet]
        public ActionResult ModificarPropuesta(int id)
        {
            if (SesionServicio.UsuarioSesion == null)
            {
                return Redirect("/Home/Index");
            }
            else
            {
                return View(propuestas.ObtenerPropuestaPorId(id));
            }
        }

        [HttpPost]
        public ActionResult Modificar(FormCollection form)
        {
            int IdPropuesta = int.Parse(form["idPropuesta"]);
            int TipoDonacion = int.Parse(form["TipoDonacion"]);

            Propuestas pv =propuestas.ObtenerPropuestaPorId(IdPropuesta);


            if (Request.Files.Count > 0 && Request.Files[0].ContentLength > 0)
            {
                string nombreSignificativo = form["Nombre"] + DateTime.Now.ToString();
                string pathRelativoImagen = ImagenesUtility.Guardar(Request.Files[0], nombreSignificativo);
                pv.Foto = pathRelativoImagen;
            }

            switch (TipoDonacion)
            {
                case 1: 
                    PropuestasDonacionesMonetarias pmv = pv.PropuestasDonacionesMonetarias.FirstOrDefault();

                    pmv.Nombre = form["Nombre"];
                    pmv.Descripcion = form["Descripcion"];
                    pmv.FechaFin = System.DateTime.Parse(form["FechaFin"]);
                    pmv.TelefonoContacto = form["TelefonoContacto"];
                    pmv.TipoDonacion = int.Parse(form["TipoDonacion"]);
                    pmv.Foto = pv.Foto;
                    foreach (var pr in pmv.Propuestas.PropuestasReferencias)
                    {
                        int currentIndex = pmv.Propuestas.PropuestasReferencias.ToList().IndexOf(pr);
                        pr.Nombre = form["Referencia" + currentIndex + "Nombre"];
                        pr.Telefono = form["Referencia" + currentIndex + "Telefono"];
                    }

                    pmv.Dinero = decimal.Parse(form["Dinero"]);
                    pmv.CBU = form["CBU"];

                    propuestas.ModificarPropuesta(IdPropuesta, pmv);
                    break;

                case 2:
                    pv.Nombre = form["Nombre"];
                    pv.Descripcion = form["Descripcion"];
                    pv.FechaFin = System.DateTime.Parse(form["FechaFin"]);
                    pv.TelefonoContacto = form["TelefonoContacto"];
                    pv.TipoDonacion = int.Parse(form["TipoDonacion"]);
                    pv.Foto = pv.Foto;
                    foreach (var pr in pv.PropuestasReferencias)
                    {
                        int currentIndex = pv.PropuestasReferencias.ToList().IndexOf(pr);
                        pr.Nombre = form["Referencia" + currentIndex + "Nombre"];
                        pr.Telefono = form["Referencia" + currentIndex + "Telefono"];
                    }

                    List<PropuestasDonacionesInsumos> ListaInsumos = ListaDeInsumos(form);

                    propuestas.Modificar(pv, ListaInsumos);
                    break;

                case 3: 
                    PropuestasDonacionesHorasTrabajo pht = pv.PropuestasDonacionesHorasTrabajo.FirstOrDefault();

                    pht.Nombre = form["Nombre"];
                    pht.Descripcion = form["Descripcion"];
                    pht.FechaFin = System.DateTime.Parse(form["FechaFin"]);
                    pht.TelefonoContacto = form["TelefonoContacto"];
                    pht.TipoDonacion = int.Parse(form["TipoDonacion"]);
                    pht.Foto = pv.Foto;
                    foreach (var pr in pht.Propuestas.PropuestasReferencias)
                    {
                        int currentIndex = pht.Propuestas.PropuestasReferencias.ToList().IndexOf(pr);
                        pr.Nombre = form["Referencia" + currentIndex + "Nombre"];
                        pr.Telefono = form["Referencia" + currentIndex + "Telefono"];
                    }

                    pht.CantidadHoras = int.Parse(form["CantidadHoras"]);
                    pht.Profesion = form["Profesion"];

                    propuestas.ModificarPropuesta(IdPropuesta, pht);
                    break;
            }

            return Redirect("/Home/Index");
        }
    }
}


