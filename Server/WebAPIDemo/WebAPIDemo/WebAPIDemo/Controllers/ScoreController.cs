using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebAPIDemo.Controllers
{
    public class ScoreController : ApiController
    {
       
        public string Get( int score, string name,int isFinished)
        {
            DatabaseManager dbMan = new DatabaseManager();
           string res = dbMan.SetScore(score,name,isFinished);
            return res;
        }



    }
}