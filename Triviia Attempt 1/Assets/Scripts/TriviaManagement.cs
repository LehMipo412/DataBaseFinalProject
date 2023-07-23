using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TriviaManagement : MonoBehaviour
{
    
    public GameObject[] questions;
    public AnAnswer answerScript;
    public QuestionManager theQuestionManager;
    public int index;

    // Start is called before the first frame update
    void Start()
    {
        index = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ChangeToNextQuestion()
    {
        answerScript.gameObject.SetActive(false);
        index++;

        answerScript = questions[index].GetComponent<AnAnswer>();
        answerScript.SetText(theQuestionManager.Question_text.text);
        answerScript.answers[0].GetComponent<TMP_Text>().text = theQuestionManager.Ans1_text.text;
        answerScript.answers[1].GetComponent<TMP_Text>().text = theQuestionManager.Ans2_text.text;
        answerScript.answers[2].GetComponent<TMP_Text>().text = theQuestionManager.Ans3_text.text;
        answerScript.answers[3].GetComponent<TMP_Text>().text = theQuestionManager.Ans4_text.text;

      //  theQuestionManager.InstantiateQuestion(index + 1);
        Debug.Log(index);
        Debug.Log(theQuestionManager.Question_text.text);
       
        answerScript.gameObject.SetActive(true);

        
    }
}
