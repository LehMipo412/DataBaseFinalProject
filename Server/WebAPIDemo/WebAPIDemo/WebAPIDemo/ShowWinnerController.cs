using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebAPIDemo.Controllers
{
    public class ShowWinnerController : ApiController
    {
        public string Get()
        {
            DatabaseManager dbMan = new DatabaseManager();
            string res = dbMan.ShowwWinner();
            //actually do that
            return res;
        }

       
    }
}