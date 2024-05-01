using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using System;

public class MainMenu : MonoBehaviour
{
    //Panels
    public GameObject feedbackPanel;

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

    public void back()
    { feedbackPanel.SetActive(false);}


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


}
