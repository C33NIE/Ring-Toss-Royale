using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using System;
using JetBrains.Annotations;
using System.Linq;

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

    public Image img;

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
                string responseText = www.downloadHandler.text;
                string[] data = responseText.Split('\t');
                if (data[0] == "0")
                {
                    if (int.TryParse(data[1], out int P_ID))
                    {
                        Debug.Log("User login successful. P_ID: " + P_ID);
                        DBManager.username = Logemailfield.text;
                        DBManager.P_ID = P_ID;
                        DBManager.username = data[2];
                        DBManager.wins = int.Parse(data[3]);
                        DBManager.losses = int.Parse(data[4]);
                        DBManager.ringID= int.Parse(data[5]);
        

                    // Add any other actions for successful login
                         UnityEngine.SceneManagement.SceneManager.LoadScene(1);

                    }
                    else
                    {
                        Debug.LogError("Failed to parse P_ID from server response. Server response: " + responseText);
                    }
                }
                else if (data.Length >= 2 && data[0] != "0")
                {
                    Debug.Log("User login failed. Error: " + responseText);
                }
                else
                {
                    Debug.LogError("Unexpected server response: " + responseText);
                }
            }
    }

    /*public IEnumerator getRing(string ItemID)
    {
        WWWForm form = new WWWForm();
        form.AddField("RingID", ItemID);

        UnityWebRequest www = UnityWebRequest.Post("http://localhost/unity/getring.php", form);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            byte[] bytes = www.downloadHandler.data;

            Debug.Log("Image data length: " + bytes.Length);

            Texture2D texture = new Texture2D(10, 10);
            texture.LoadImage(bytes);

            Debug.Log("Texture size: " + texture.width + "x" + texture.height);

            // Check pixel values
            Color32[] pixels = texture.GetPixels32();
            foreach (Color32 pixel in pixels)
            {
                if (pixel.a != 0)
                {
                    Debug.Log("Pixel color: " + pixel);
                    break;
                }
            }

            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            // Set the currentRingSprite in DBManager
            DBManager.currentRing = sprite;

            img.sprite = DBManager.currentRing;
        }
    }*/




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
