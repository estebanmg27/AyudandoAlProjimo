using AyudandoAlProjimo.Data;
using AyudandoAlProjimo.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace WebApi.Controllers
{
    public class ApiDonacionesController : ApiController
    {
        // GET: ApiDonaciones

        DonacionServicio donaciones = new DonacionServicio();
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        public List<ApiDonaciones> Get(int id)
        {
            return donaciones.HistorialDonaciones(id);
        }


        public void Post([FromBody]string value)
        {
        }

        public void Put(int id, [FromBody]string value)
        {
        }

        public void Delete(int id)
        {
        }
    }
}
