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

            List<Propuestas> propuestas = (from prop in ctx.Propuestas
                                         where prop.IdPropuesta == x &&
                                         prop.Estado == 0
                                         select prop).ToList();

            foreach (var lista in propuestas)
            {
                lista.Estado = 2; // 2 = no activa
            }
        }

        public void NoAceptarDenuncia(int x)
        {
            List<Denuncias> denuncias = (from d in ctx.Denuncias
                                         where d.IdDenuncia == x &&
                                         d.Estado == 0
                                         select d).ToList();

            foreach (var lista in denuncias)
            {
                lista.Estado = 1; // 1 = activa
            }

            ctx.SaveChanges();
        }

        public List<Denuncias> ObtenerDenunciasPendientes()
        {
            List<Denuncias> denunciasPendientes =  (from denuncias in ctx.Denuncias
                                                    where denuncias.Estado == 0 // 0 = pendiente
                                                    orderby denuncias.IdDenuncia descending
                                                    select denuncias).ToList();

            return denunciasPendientes;
        }
    
    }
}
