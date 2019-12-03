using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AyudandoAlProjimo.Data;
using AyudandoAlProjimo.Data.Extensiones;
using AyudandoAlProjimo.Servicios;

namespace WebApi.Controllers
{
    public class HistorialDonacionesController : ApiController
    {
        // GET: api/HistorialDonaciones
        private DonacionServicio _donacionServicio = new DonacionServicio();
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/HistorialDonaciones/1
        public List<ApiDonaciones> Get(int id)
        {

            return _donacionServicio.MisDonacionesId(id);

        }

        // POST: api/HistorialDonaciones
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/HistorialDonaciones/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/HistorialDonaciones/5
        public void Delete(int id)
        {
        }
    }
}