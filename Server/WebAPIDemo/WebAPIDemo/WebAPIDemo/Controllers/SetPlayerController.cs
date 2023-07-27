using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebAPIDemo.Controllers
{
    public class SetPlayerController : ApiController
    {
        
        
        public string Get(string name)
        {
            DatabaseManager dbman = new DatabaseManager();
            string result = dbman.SetPlayer(name);
            return result;
        }

        
    }
}