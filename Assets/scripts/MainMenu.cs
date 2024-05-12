using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using System;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    //Panels
    public GameObject feedbackPanel;
    public GameObject profilepanel;
    public GameObject shopPanel;


    //texts
    public TMP_Text playerNameText;
    public TMP_Text winsText;
    public TMP_Text lossesText;
    public TMP_Text mostUsedRingText;

    //inputfield
    public TMP_InputField feedinput;


    public void feedbacksender()
    {
        StartCoroutine(feedbacking());
    }

    public void ShowFeedbackPanel()
    {
        feedbackPanel.SetActive(true);
    }

    public void ShowProfilepanel()
    {
        profilepanel.SetActive(true);
    }

    public void ShowshopPanel() { 
    shopPanel.SetActive(true);
    }

    public void back()
    { feedbackPanel.SetActive(false);
      profilepanel.SetActive(false);
      shopPanel.SetActive(false);  
    }


    public void playbuttonpressed()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }

    public IEnumerator feedbacking()
    {
        WWWForm form = new WWWForm();
        form.AddField("content", feedinput.text);
        form.AddField("P_ID", DBManager.P_ID);
        UnityWebRequest www = UnityWebRequest.Post("http://localhost/unity/Feedback.php", form);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("feedback stored! ");
        }
        else
        {
            Debug.Log("User creation failed: " + www.error);
        }
    }

    void Start()
    {

        // Ensure all UI Text components are assigned in the Inspector
        if (playerNameText == null || winsText == null || lossesText == null || mostUsedRingText == null)
        {
            Debug.LogError("One or more UI Text components are not assigned!");
            return;
        }

        // Display player details from DBManager
        playerNameText.text = DBManager.username;
        winsText.text = "Wins: " + DBManager.wins.ToString();
        lossesText.text = "Losses: " + DBManager.losses.ToString();
    }
}
