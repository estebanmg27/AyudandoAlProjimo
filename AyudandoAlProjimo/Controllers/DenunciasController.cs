using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AyudandoAlProjimo.Data;
using AyudandoAlProjimo.Servicios;

namespace AyudandoAlProjimo.Controllers
{
    public class DenunciasController : Controller
    {
        DenunciaServicio denuncias = new DenunciaServicio();

        // GET: Denuncias
        public ActionResult ListaDeDenuciasARevisar()
        {
            List<Denuncias> lista = denuncias.ObtenerDenunciasPendientes();
            return View(lista);
        }

        public ActionResult Aceptar(int id)
        {
            denuncias.AceptarDenuncia(id);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult NoAceptar(int id)
        {
            denuncias.NoAceptarDenuncia(id);
            return RedirectToAction("Index", "Home");
        }
    }
}