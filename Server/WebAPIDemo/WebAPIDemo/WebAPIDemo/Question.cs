using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPIDemo
{
    [System.Serializable]
    public class Question
    {
        public string text;
        public string ans1;
        public string ans2;
        public string ans3;
        public string ans4;
        public int correctID;
    }
}