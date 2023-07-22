using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebAPIDemo.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<int> Get()
        {
            return new int[] { 4,85,7 };
        }

        // GET api/values/5
        public string Get(int id)
        {
            DatabaseManager dbman = new DatabaseManager();
            string playerName = dbman.GetPlayerName(id);
            return playerName;
        }

        // POST api/values
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
