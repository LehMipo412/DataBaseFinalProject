﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebAPIDemo.Controllers
{
    public class ShowWinnerController : ApiController
    {
        // GET api/<controller>
       

        // GET api/<controller>/5
        public string Get()
        {
            DatabaseManager dbMan = new DatabaseManager();
            string res = dbMan.ShowwWinner();
            //actually do that
            return res;
        }

        // POST api/<controller>
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}