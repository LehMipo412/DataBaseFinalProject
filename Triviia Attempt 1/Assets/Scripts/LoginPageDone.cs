using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;

public class LoginPageDone : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_InputField playerNameInputField;
    public GameObject LoginPage;
    public GameObject GameBoard;
    private string playerName;
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        playerName = playerNameInputField.text;
    }

    public void Submitted()
    {
        StartCoroutine(SetPlayer());
        LoginPage.SetActive(false);
        GameBoard.SetActive(true);
    }
    IEnumerator SetPlayer()
    {
        UnityWebRequest www = UnityWebRequest.Get("https://localhost:44330/api/SetPlayer?name=" + playerName);


        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log(www.downloadHandler.text);



           
        }
    }
}
