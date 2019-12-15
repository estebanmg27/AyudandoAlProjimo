using AyudandoAlProjimo.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AyudandoAlProjimo.Servicios
{
    public class DenunciaServicio
    {
        Entities ctx = new Entities();

        public void AceptarDenuncia(int x)
        {
            Propuestas p = ctx.Propuestas.Find(x);
            p.Estado = 2;
            ctx.SaveChanges();
            CambiarANoActiva(x);
    
        }

        public void NoAceptarDenuncia(int x)
        {
            //List<Denuncias> denuncias = (from d in ctx.Denuncias
            //                             where d.IdDenuncia == x &&
            //                             d.Estado == 0
            //                             select d).ToList();

            //foreach (var lista in denuncias)
            //{
            //    lista.Estado = 1;
            //}
            Propuestas p = ctx.Propuestas.Find(x);
            p.Estado = 2;
            ctx.SaveChanges();
            CambiarAActiva(x);
        }

        public List<Denuncias> ObtenerDenunciasPendientes()
        {
            List<Denuncias> denunciasPendientes =  (from denuncias in ctx.Denuncias
                                                    where denuncias.Estado == 0 // 0 = pendiente
                                                    orderby denuncias.IdDenuncia ascending
                                                    select denuncias).ToList();

            return denunciasPendientes;
        }

        public void CambiarANoActiva(int id)
        {
            List<Denuncias> lista = (from d in ctx.Denuncias
                                     where d.IdPropuesta == id
                                     select d).ToList();
            foreach (var o in lista)
            {
                o.Estado = 2;
            }
            ctx.SaveChanges();
        }

        public void CambiarAActiva(int id)
        {
            List<Denuncias> lista = (from d in ctx.Denuncias
                                     where d.IdPropuesta == id
                                     select d).ToList();
            foreach (var o in lista)
            {
                o.Estado = 0;
            }
            ctx.SaveChanges();
        }
    }
}
