using AyudandoAlProjimo.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AyudandoAlProjimo.Controllers
{
    public class DonacionesController : Controller
    {
        SesionServicio sesion = new SesionServicio();
        // GET: Donaciones
        public ActionResult MiHistorialDonaciones()
        {
            ViewBag.UsuarioId = SesionServicio.UsuarioSesion.IdUsuario;
            return View();
        }
    }
}