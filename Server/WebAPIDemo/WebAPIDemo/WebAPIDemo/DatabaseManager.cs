using MySql.Data.MySqlClient;
using System;

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
        string playerName;
        

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
        public string SetScore(int score, string name,int isfinnished)
        {
            string result;
            try
            {
                Connect();

                string query = "UPDATE player SET Score ="+score+ ",IsFinished ="+isfinnished+"  WHERE Name ='" + name+"'";

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
                playerName = name;
            }
            catch
            {
                Disconnect();
                return "Failure";
            }
            return result;
        }
        public bool CanStartGame()
        {
            int counter = 0;
            try
            {
                Connect();

                string query = "SELECT * FROM player WHERE name IS NOT NULL";

                cmd = new MySqlCommand(query, con);
                reader = cmd.ExecuteReader();
                foreach(var player in reader)
                {
                    counter++;
                }

                if(counter%2 == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
               

            }
            catch
            {
                Disconnect();
                return false;   
            }
           

            
        }
        public string ShowwWinner()
        {
            string name1 = null;
            string name2 = null;
            int score1 = -1;
            int score2 = -1;

            try
            {
                Connect();

                string query = "SELECT Name FROM `final projet`.player ORDER BY PlayerID DESC LIMIT 2;";

                cmd = new MySqlCommand(query, con);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (name1 == null)
                    {
                        name1 = reader.GetString(0);
                        continue;
                    }
                    if (name2 == null)
                    {
                        name2 = reader.GetString(0);
                        continue;
                    }
                }
                Disconnect();
                Connect();

                query = "SELECT Score FROM `final projet`.player ORDER BY PlayerID DESC LIMIT 2;";

                cmd = new MySqlCommand(query, con);
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    if (score1 == -1)
                    {
                        score1 = reader.GetInt32(0);
                        continue;
                    }
                    if (score2 == -1)
                    {
                        score2 = reader.GetInt32(0);
                        continue;
                    }
                }
                if (score1>score2)
                {
                    return $"The winner who can now enter China is: {name1} with the score of {score1}." +
                        $" better luck next time {name2}, your score was: {score2}";

                }
                else
                {

                    if(score1<score2)
                    {
                        return $"The winner who can now enter China is: {name2} with the score of {score2}." +
                        $" better luck next time {name1}, your score was: {score1}"; ;
                    }
                    else
                    {
                        return $"A draw with the score of {score2}";
                    }

                }
                


            }
            catch
            {
                
                Disconnect();
                return "failure";
            }

            
        }
        public bool CanFinishGame()
        {
            string s = null;
            string s2 = null;
            int counter1 = 0;
            int counter2 = 0;
            try
            {
                Connect();

                string query = "SELECT IsFinished FROM `final projet`.player ORDER BY playerID DESC LIMIT 2;";

                cmd = new MySqlCommand(query, con);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if ( s== null)
                    {
                        s = reader.GetString("IsFinished");
                        counter1 = int.Parse(s);
                        continue;
                    }
                    if (s2 == null)
                    {
                        s2 = reader.GetString("IsFinished");
                        counter2 = int.Parse(s2);
                        continue;
                    }

                    
                    
                }
                if (counter1 + counter2 == 2)
                {
                    return true;
                }
                else
                {
                    return false;
                }
                


            }
            catch
            {
                Disconnect();
                return false;
            }



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
           
            string query = "SELECT * FROM answers WHERE ContainedQuestionID = " + QuestionID;
            cmd = new MySqlCommand(query, con);
            reader = cmd.ExecuteReader();
            
                while (reader.Read())
                {
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
                
            }

            Disconnect();
        }
        public Question AccuireQuestion()
        {
            return question;
        }
    }
    
}