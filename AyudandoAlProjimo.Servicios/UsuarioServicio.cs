using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using AyudandoAlProjimo.Data;

namespace AyudandoAlProjimo.Servicios
{
    public class UsuarioServicio
    {
        Entities ctx = new Entities();

        public void AgregarUsuario(Usuarios u)
        {
            u.Activo = false;
            u.Token = Guid.NewGuid().ToString("N").Substring(2);
            u.FechaCracion = DateTime.Today;
            u.TipoUsuario = 2;
            ctx.Usuarios.Add(u);
            ctx.SaveChanges();

            EnviarCorreo(u);
        }

        public void ActivarCuenta(String token)
        {
            Usuarios usuarios = ctx.Usuarios.First(u => u.Token == token);
            usuarios.Activo = true;
            ctx.SaveChanges();
        }

        public Usuarios Autorizar(Usuarios usuario)
        {
            return ctx.Usuarios.Where(x => x.Email == usuario.Email && x.Password == usuario.Password).FirstOrDefault();
        }

        public void EnviarCorreo(Usuarios u)
        {
            try
            {
                MailMessage correo = new MailMessage();
                correo.From = new MailAddress("ayudandoalprojimo2019pw3@gmail.com");
                correo.To.Add(u.Email);
                correo.Subject = "Activar usuario";
                correo.Body = HttpContext.Current.Request.Url.Scheme.ToString() + "://" + HttpContext.Current.Request.Url.Authority.ToString() + "/Usuario/Activar?token=" + u.Token;
                correo.IsBodyHtml = true;
                correo.Priority = MailPriority.Normal;

                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 25;
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = true;
                string scuentaCorreo = "ayudandoalprojimo2019pw3@gmail.com";
                string spasswordCorreo = "ayudandoalprojimo2019";
                smtp.Credentials = new System.Net.NetworkCredential(scuentaCorreo, spasswordCorreo);

                smtp.Send(correo);
            }
            catch (Exception e)
            {
                string error = e.Message;
            }
        }

        public void ModificarPerfil(Usuarios user)
        {

            var usuario = BuscarUsuarioPorId(user.IdUsuario);

            if (string.IsNullOrEmpty(user.UserName))
            {
                var nombreDeUsuario = $"{user.Nombre}.{user.Apellido}";
                var usuarioExistente = ctx.Usuarios.Where(x => x.UserName == nombreDeUsuario).FirstOrDefault() != null;

                if (usuarioExistente)
                {
                    Usuarios duplicado = ctx.Usuarios.Where(x => x.UserName == nombreDeUsuario).FirstOrDefault();
                }

                int numeroUsuario = 1;
                while (ctx.Usuarios.FirstOrDefault(u => u.UserName == nombreDeUsuario + numeroUsuario) != null)
                {
                    numeroUsuario++;
                }

                usuario.UserName = nombreDeUsuario + numeroUsuario;
            }

            usuario.Nombre = user.Nombre;
            usuario.Apellido = user.Apellido;
            usuario.FechaNacimiento = user.FechaNacimiento;
            usuario.Foto = user.Foto;
            ctx.SaveChanges();
        }

        public Usuarios BuscarUsuarioPorId(int id)
        {
            return ctx.Usuarios.Find(id);
        }
    }
}