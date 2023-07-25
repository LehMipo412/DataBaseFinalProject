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
    [SerializeField] public TMPro.TMP_Text timerText;
    public string startingScore;
    public string timerStringStartingPoint;
    public int score;
    public float questionTimer;
    public int isFinished;
    public bool canfinish;
    public Question question;
    public float commandTimer;
    [SerializeField] public int theCorrectAnswerID;
    public GameObject[] answers;
    public GameObject waitingRoom;
    public GameObject afterFinish;
    public GameObject theGameBoard;



    [SerializeField] public int currentQuestion;

    // Start is called before the first frame update
    void Start()
    {
        commandTimer = 1;
        canfinish = false;
        isFinished = 0;
        questionTimer = 10f;
        timerStringStartingPoint = timerText.text;
        startingScore = scoreText.text;
        score = 0;
        scoreText.text = startingScore + score;
        currentQuestion = 0;
        InstantiateQuestion();
        
    }
    private void Update()
    {
        questionTimer -= Time.deltaTime;
        commandTimer -= Time.deltaTime;

        timerText.text = timerStringStartingPoint + (int)questionTimer;
        if(questionTimer <= 0)
        {
            InstantiateQuestion();
            questionTimer = 10;
        }
        if (isFinished == 1)
        {
            if (canfinish == false)
            {
                
                if (commandTimer <= 0)
                {
                    CheckIfBothFinished();
                    commandTimer = 1;
                }
            }
            if(canfinish == true)
            {
                afterFinish.SetActive(true);
                
            }
        }
    }

    public void CheckIfBothFinished()
    {
        StartCoroutine(CanFinishGame());
    }
    public void ShowShowwWinner()
    {

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


            questionTimer = 10;
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
            score+= (int)questionTimer;
            scoreText.text = startingScore + score;
        }
    }
    IEnumerator SetScoreToDB()
    {
        Debug.Log("Started");
        UnityWebRequest www = UnityWebRequest.Get("https://localhost:44330/api/Score?Score="+(score+1)+"&name="+LoginPageDone.playerName+ "&isFinished="+isFinished);
        Debug.Log("Connected");
        yield return www.SendWebRequest();
        Debug.Log("command happened");
        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            
        }
    }
    IEnumerator CanFinishGame()
    {
        Debug.Log("Started");
        UnityWebRequest www = UnityWebRequest.Get("https://localhost:44330/api/CanFinishGame");
        Debug.Log("Connected");
        yield return www.SendWebRequest();
        Debug.Log("command happened");
        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            string ans = www.downloadHandler.text;
            canfinish = bool.Parse(ans);
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
        Debug.Log("current question: " + currentQuestion);
        StartCoroutine(SetScoreToDB());
        currentQuestion++;
        if(currentQuestion>5)
        {
            isFinished = 1;
            StartCoroutine(SetScoreToDB());
            LoginPageDone.canStartGame = false;
            //theGameBoard.SetActive(false);
            waitingRoom.SetActive(true);
        }
        else
            StartCoroutine(GetQuestion(currentQuestion));
    }


}
