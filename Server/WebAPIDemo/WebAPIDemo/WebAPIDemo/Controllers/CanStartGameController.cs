using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebAPIDemo.Controllers
{
    public class CanStartGameController : ApiController
    {
        
        public bool Get()
        {
            DatabaseManager dbman = new DatabaseManager();
            bool canStart = dbman.CanStartGame();
            return canStart;
        }

     
    }
}