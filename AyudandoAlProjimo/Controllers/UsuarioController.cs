using System;
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
        PropuestaServicio propuestas = new PropuestaServicio();
        SesionServicio sesion = new SesionServicio();
        public ActionResult Inicio()
        {
            var MasValoradas = propuestas.ObtenerCincoPropuestasMasValoradas();
            return View(MasValoradas);
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
            if (usuarios.MailExistente(u).Count == 1)
            {
                ModelState.AddModelError("Email", "Ya hay un usuario registrado con ese email");
            }
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

        [HttpPost]
        public ActionResult AutorizarLogin(Usuarios u)
        {
            var detalleUsuario = usuarios.Autorizar(u);
            if (!ModelState.IsValid)
            {
                return View(u);
            }
            if (detalleUsuario == null)
            {
                return Redirect("/Usuario/Login");
            }
            var usuario = usuarios.ObtenerUsuario(u);
            if (usuario.Activo == false)
            {
                ViewBag.MotivoError = "Activa tu cuenta en la casilla de mail";
                return View("../Shared/Error");
            }
            else
            {
                //Session["IdUsuario"] = detalleUsuario.IdUsuario;
                SesionServicio.UsuarioSesion = detalleUsuario;
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

        public ActionResult MiPerfil()
        {

            Usuarios u = SesionServicio.UsuarioSesion;
            return View(u);
        }

        public ActionResult VerMiPerfil()
        {
            Usuarios u = SesionServicio.UsuarioSesion;

            if (SesionServicio.UsuarioSesion != null)
            {
                return View(u);
            }
            else
            {
                return View("Inicio");
            }
        }

        [HttpPost]
        public ActionResult MiPerfil(Usuarios user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }

             usuarios.ModificarPerfil(user);
             return RedirectToAction("Index", "Home");    
        }
    }
}