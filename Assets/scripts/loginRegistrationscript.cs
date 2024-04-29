using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class loginRegistrationscript : MonoBehaviour
{
    //panels
    public GameObject registrationPanel;
    public GameObject loginPanel;

    //inputfields
    public TMP_InputField Regemailfield;
    public TMP_InputField Regpasswordfield;
    public TMP_InputField Logemailfield;
    public TMP_InputField Logpasswordfield;

    //buttons
    public Button loginButton;
    public Button RegistrationButton;

    //texts
    public Text player_display; 

    private void Start()
    {
        if(DBManager.LoggedIn)
        {
            player_display.text = "Player: " + DBManager.username;
        }
    }


    public void CallRegister()
    {
        StartCoroutine(Register());
    }

    public void CallLogin()
    {
        StartCoroutine(login());
    }

    public IEnumerator login()
    {
        WWWForm form = new WWWForm();
        form.AddField("email", Logemailfield.text);
        form.AddField("password", Logpasswordfield.text);
        UnityWebRequest www = UnityWebRequest.Post("http://localhost/unity/login.php", form);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error: " + www.error);
        }
        else
        {
            if (www.downloadHandler.text.Trim() == "0")
            {
                Debug.Log("User login successful.");
                DBManager.username = Logemailfield.text;
                // Add any other actions for successful login
                UnityEngine.SceneManagement.SceneManager.LoadScene(1);
            }
            else
            {
                Debug.Log("User login failed. Error: " + www.downloadHandler.text);
            }
        }

    }


    public IEnumerator Register()
    {
        WWWForm form = new WWWForm();
        form.AddField("email", Regemailfield.text);
        form.AddField("password", Regpasswordfield.text);
        UnityWebRequest www = UnityWebRequest.Post("http://localhost/unity/InsertData.php", form);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("User created successfully");
        }
        else
        {
            Debug.Log("User creation failed: " + www.error);
        }
    }



    public void VerifyInput()
    {
        RegistrationButton.interactable = (Regemailfield.text.Length >= 0 && Regpasswordfield.text.Length >= 0);
    }


    public void ShowRegistrationPanel()
    {
        registrationPanel.SetActive(true);
        loginPanel.SetActive(false);
    }

    public void ShowLoginPanel()
    {
        registrationPanel.SetActive(false);
        loginPanel.SetActive(true);
    }

    public void back()
    {
        registrationPanel.SetActive(false);
        loginPanel.SetActive(false);
    }
}
