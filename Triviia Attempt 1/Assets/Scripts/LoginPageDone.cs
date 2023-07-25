using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using System;

public class LoginPageDone : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_InputField playerNameInputField;
    public GameObject LoginPage;
    public GameObject WaitingRoomBoard;
    public GameObject GameBoard;
    public GameObject triviaManager;
    public static string playerName;
    public bool isSubmitted;
    public static bool canStartGame;
    public float timer;
    void Start()
    {
        timer = 1;
        canStartGame = false;
        isSubmitted = false;
        
    }


    // Update is called once per frame
    void Update()
    {
        playerName = playerNameInputField.text;
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            if (isSubmitted == true)
            {
                if (canStartGame == false)
                {
                    CheckOtherConnections();
                    timer = 1;
                }
                else
                {
                    isSubmitted = false;
                }
            }
        }
        if (isSubmitted == true)
        {
            if (canStartGame == true)
            {
                WaitingRoomBoard.SetActive(false);
                GameBoard.SetActive(true);

            }
            else
            {
                GameBoard.SetActive(false);

            }
        }
    }

    public void CheckOtherConnections()
    {
        StartCoroutine(CanStartGame());
        
    }
    public void Submitted()
    {
        StartCoroutine(SetPlayer());
        isSubmitted = true;
        LoginPage.SetActive(false);
        triviaManager.SetActive(true);
        WaitingRoomBoard.SetActive(true);
       
    }
    IEnumerator CanStartGame()
    {
        Debug.Log("Started");
        UnityWebRequest www = UnityWebRequest.Get("https://localhost:44330/api/CanStartGame");
        Debug.Log("Connected");
        yield return www.SendWebRequest();
        Debug.Log("command happened");
        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("still Connected");
            string ans = www.downloadHandler.text;
            Debug.Log(ans);
            canStartGame = bool.Parse(ans);
        }
    }
    IEnumerator SetPlayer()
    {
        UnityWebRequest www = UnityWebRequest.Get("https://localhost:44330/api/SetPlayer?name=" + playerName);
        

        yield return www.SendWebRequest();
        Debug.Log("changed name");

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
