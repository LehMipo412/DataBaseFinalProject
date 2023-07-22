using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriviaManagement : MonoBehaviour
{
    
    public GameObject[] questions;
    public AnAnswer answerScript;
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
        answerScript.gameObject.SetActive(true);

        
    }
}
