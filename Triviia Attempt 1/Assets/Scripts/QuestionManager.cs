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
    [SerializeField] public int currentQuestion;

    // Start is called before the first frame update
    void Start()
    {
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
            Debug.Log("Connected");
            Debug.Log(www.downloadHandler.text);



            Question question = JsonUtility.FromJson<Question>(www.downloadHandler.text);
            Debug.Log(question.text);
            if (question != null)
            {
                Question_text.text = question.text;
                Ans1_text.text = question.ans1;
                Ans2_text.text = question.ans2;
                Ans3_text.text = question.ans3;
                Ans4_text.text = question.ans4;
                Debug.Log(question.correctID.ToString());
            }
        }
    }
    public void InstantiateQuestion()
    {
        currentQuestion++;
        StartCoroutine(GetQuestion(currentQuestion));
    }

}
