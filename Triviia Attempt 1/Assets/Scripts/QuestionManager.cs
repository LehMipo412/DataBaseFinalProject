using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class QuestionManager : MonoBehaviour
{

    [SerializeField] public TMPro.TMP_Text Question_text;
    [SerializeField] public TMPro.TMP_Text Ans1_text;
    [SerializeField] public TMPro.TMP_Text Ans2_text;
    [SerializeField] public TMPro.TMP_Text Ans3_text;
    [SerializeField] public TMPro.TMP_Text Ans4_text;
    [SerializeField] public TMPro.TMP_Text scoreText;
    public string startingScore;
    public int score;
    public Question question;
    [SerializeField] public int theCorrectAnswerID;
    public GameObject[] answers;

    [SerializeField] public int currentQuestion;

    // Start is called before the first frame update
    void Start()
    {
        startingScore = scoreText.text;
        score = 0;
        scoreText.text = startingScore + score;
        currentQuestion = 0;
        InstantiateQuestion();
        
    }


    IEnumerator GetQuestion(int id)
    {
        UnityWebRequest www = UnityWebRequest.Get("https://localhost:44330/api/GetQuestion/" + id);
        

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            // Show results as text
            //Debug.Log("Connected");
            //Debug.Log(www.downloadHandler.text);



             question = JsonUtility.FromJson<Question>(www.downloadHandler.text);
            //Debug.Log(question.text);
            if (question != null)
            {
                Question_text.text = question.text;
                Ans1_text.text = question.ans1;
                Ans2_text.text = question.ans2;
                Ans3_text.text = question.ans3;
                Ans4_text.text = question.ans4;
                theCorrectAnswerID = question.correctID;
                Debug.Log(theCorrectAnswerID.ToString());
                Debug.Log($"the answers ids are{question.answersID[0]},{question.answersID[1]},{question.answersID[2]},{question.answersID[3]}");

            }
        }
    }

    public void IsCorrectAnswer (GameObject theAnswerButtton)
    {
        int selectedAnswer = 0;
        for (int i = 0; i < answers.Length; i++)
        {
            if(answers[i] == theAnswerButtton)
            {
                selectedAnswer = i;
                Debug.Log("found: "+ selectedAnswer);
                GiveCorrectIDLocation();
                
            }
            
        }
        if (question.answersID[selectedAnswer] == question.correctID)
        {
            Debug.Log("correct");
            score++;
            scoreText.text = startingScore + score;
        }
    }
    public void GiveCorrectIDLocation()
    {
        for (int i = 0; i < question.answersID.Length; i++)
        {
            if(question.answersID[i] == question.correctID)
            {
                Debug.Log("the number that you supposed to get: " + i);
            }
        }
    }
    public void InstantiateQuestion()
    {
        currentQuestion++;
        if(currentQuestion>5)
        {
            gameObject.SetActive(false);
        }
        else
            StartCoroutine(GetQuestion(currentQuestion));
    }


}
