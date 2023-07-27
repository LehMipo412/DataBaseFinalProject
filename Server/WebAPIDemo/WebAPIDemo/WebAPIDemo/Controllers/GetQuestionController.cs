using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebAPIDemo.Controllers
{
    public class GetQuestionController : ApiController
    {
        
        public Question Get(int id)
        {
            DatabaseManager dbman = new DatabaseManager();
            dbman.GetQuestion(id);
            dbman.GetAnswers(id);
            Question updatedquestion = dbman.AccuireQuestion();
            return updatedquestion;
        }
    }
}