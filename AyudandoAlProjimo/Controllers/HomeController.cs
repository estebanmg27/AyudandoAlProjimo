﻿using System;
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
            Usuarios u = SesionServicio.UsuarioSesion;
            List<Propuestas> PropuestasLista = propuestas.ObtenerPropuestasMenosLasPropias(u);
            return View(PropuestasLista);
        }

        public ActionResult About()
        {
            return View();
        }
    }
}