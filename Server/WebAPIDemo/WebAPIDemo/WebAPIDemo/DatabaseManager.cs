using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace WebAPIDemo
{
    public class DatabaseManager
    {
        string con_str = "Server=localhost; database= final projet; UID=root; password=mchvbhbdv";
        MySqlConnection con;
        MySqlCommand cmd;
        MySqlDataReader reader;
        Question question = new Question();
        string[] ansArr = new string[4];

        private void Disconnect()
        {
            con.Close();
        }
        private void Connect()
        {
            con = new MySqlConnection(con_str);
            try 
            {
                con.Open();
            }
            catch
            {
                con.Close();
            }
        }

        internal string SetPlayer(string name)
        {
            string result;
            try
            {
                Connect();
                string query = "INSERT INTO player(Name) values('" + name  + "')";      
                cmd = new MySqlCommand(query, con);
                reader = cmd.ExecuteReader();
                result = "Success";

            }
            catch
            {
                Disconnect();
                return "Failure";
            }
            return result;
        }

        public string GetPlayerName(int playerId)
        {
            try
            {
                Connect();
                string query = "SELECT Name FROM Players WHERE PlayerID =" + playerId;
                cmd = new MySqlCommand(query, con);
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    string name = reader.GetString("Name");
                    Disconnect();
                    return name;
                }
                else
                {
                    Disconnect();
                    return null;
                }
            }
            catch
            {
                Disconnect();
                return null;
            }
            
        }

        public void GetQuestion(int QuestionID)
        {
            try
            {
                Connect();
                
                string query = "SELECT * FROM question WHERE QuestionID =" + QuestionID;
                cmd = new MySqlCommand(query, con);
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    string theText = reader.GetString("QuestionText");



                    
                    
                    question.text = reader.GetString("QuestionText");
                    question.correctID = reader.GetInt32("CorrectAnswerID");
                    

                    
                    Disconnect();
                    Console.WriteLine(question.text);
                   

                }
                else
                {
                    Disconnect();
                   
                }

                

            }
            catch
            {
                Disconnect();
                
            }

        }
        public void GetAnswers(int QuestionID)
        {
            Connect();
            //string query = "SELECT AnswerText FROM answers WHERE ContainedQuestionID = " + QuestionID;
            string query = "SELECT * FROM answers WHERE ContainedQuestionID = " + QuestionID;
            cmd = new MySqlCommand(query, con);
            reader = cmd.ExecuteReader();
            
                while (reader.Read())
                {
                //for (int i = 0; i < reader.FieldCount; i++)
                //{
                //    ansArr[i] = reader.GetValue(i).ToString();
                //}

                //string mewo = reader.GetValue(1).ToString();
                if (question.ans1 == null)
                {
                    question.ans1 = reader.GetString("AnswerText");
                    question.answersID[0] = reader.GetInt32("AnswerID");
                    continue;
                }
                if (question.ans2 == null)
                {
                    question.ans2 = reader.GetString("AnswerText");
                    question.answersID[1] = reader.GetInt32("AnswerID");

                    continue;
                }
                if (question.ans3 == null)
                {
                    question.ans3 = reader.GetString("AnswerText");
                    question.answersID[2] = reader.GetInt32("AnswerID");
                    continue;
                }
                if (question.ans4 == null)
                {
                    question.ans4 = reader.GetString("AnswerText");
                    question.answersID[3] = reader.GetInt32("AnswerID");
                    continue;
                }
                //question.ans1 = ansArr[0];
                //question.ans2 = ansArr[1];
                //question.ans3 = ansArr[2];
                //question.ans4 = ansArr[3];
            }

            Disconnect();
        }
        public Question AccuireQuestion()
        {
            return question;
        }
    }
    
}