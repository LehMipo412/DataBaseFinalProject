using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AnAnswer : MonoBehaviour
{

    public GameObject[] answers;
    public TMP_Text theText;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void SetText(string laText)
    {
        theText.text = laText;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
