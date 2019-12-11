using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AyudandoAlProjimo.Controllers
{
    public class ApiPruebaController : ApiController
    {
        // GET: api/ApiPrueba
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/ApiPrueba/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/ApiPrueba
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/ApiPrueba/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ApiPrueba/5
        public void Delete(int id)
        {
        }
    }
}
