﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AyudandoAlProjimo.Servicios;
using AyudandoAlProjimo.Data;
using System.Web;

namespace AyudandoAlProjimo.Controllers
{
    public class UsuarioController : Controller
    {

        UsuarioServicio usuarios = new UsuarioServicio();
        public ActionResult Inicio()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registro(Usuarios u)
        {
            if (!ModelState.IsValid)
            {
                return View(u);
            }
            else
            {
                usuarios.AgregarUsuario(u);
                return Redirect("/Usuario/UsuarioCreado");
            }
        }
        public ActionResult Activar(String token)
        {
            usuarios.ActivarCuenta(token);
            return Redirect("/Usuario/Login");
        }

        public ActionResult AutorizarLogin(Usuarios u)
        {
            var detalleUsuario = usuarios.Autorizar(u);
            if (detalleUsuario == null)
            {
                return Redirect("/Usuario/Login");
            }
            else
            {
                Session["IdUsuario"] = detalleUsuario.IdUsuario;
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Inicio", "Usuario");
        }

        public ActionResult UsuarioCreado()
        {
            return View();
        }
    }
}