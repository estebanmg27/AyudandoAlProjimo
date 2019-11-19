using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AyudandoAlProjimo.Servicios;
using AyudandoAlProjimo.Data;
using System.Web;

namespace AyudandoAlProjimo.Controllers
{
    public class HomeController : Controller
    {

        UsuarioServicio usuarios = new UsuarioServicio();
        PropuestaServicio propuestas = new PropuestaServicio();

        public ActionResult Index()
        {
            List<Propuestas> PropuestasLista = propuestas.ObtenerPropuestas();
            return View(PropuestasLista);
        }
    }
}