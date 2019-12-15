using AyudandoAlProjimo.Data;
using AyudandoAlProjimo.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AyudandoAlProjimo.Controllers
{
    public class ApiDonacionesController : ApiController
    {

        DonacionServicio donaciones = new DonacionServicio();

        // GET: api/ApiDonaciones
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/ApiDonaciones/5
        public List<ApiDonaciones> Get(int id)
        {
            return donaciones.HistorialDonaciones(id);
        }


        // POST: api/ApiDonaciones
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/ApiDonaciones/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ApiDonaciones/5
        public void Delete(int id)
        {
        }
    }
}
