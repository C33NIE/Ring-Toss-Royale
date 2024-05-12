using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;

public class DiscSelection : MonoBehaviour
{
    public GameObject DiscSelectionUI;
    public int SelectedDiscValue;
    private TMP_Text lastButton;
    Color myColor;

    void Start()
    {
        DiscSelectionUI.SetActive(false);
        myColor = new Color(223, 255, 0, 255);
    }

    public void SelectDisc(TMP_Text button)
    {
        lastButton = GetLastSelectedButton();
        lastButton.text = "Select";
        lastButton.color = Color.white;

        int discIndex = int.Parse(button.name.Substring(4));
        discIndex -= 1;

        PlayerPrefs.SetString("lastSelectedButtonName", button.name);
        button.text = "Selected";
        button.color = myColor;

        PlayerPrefs.SetInt("selectedDisc", discIndex);
        SelectedDiscValue = discIndex;
    }

    public TMP_Text GetLastSelectedButton()
    {
        string buttonText = PlayerPrefs.GetString("lastSelectedButtonName");
        GameObject buttonObject = GameObject.Find(buttonText);

        if (buttonObject != null)
        {
            return buttonObject.GetComponent<TMP_Text>();
        }
        else
        {
            PlayerPrefs.SetString("lastSelectedButtonName", "Text01");
            buttonText = "Text01";
            buttonObject = GameObject.Find(buttonText);
            int discIndex = 0;

            PlayerPrefs.SetInt("selectedDisc", discIndex);
            Debug.Log("Last Button Was null, now Last button is set to: " + buttonText);
            return buttonObject.GetComponent<TMP_Text>();
        }
    }
    public void openDiscUI()
    {
        DiscSelectionUI.SetActive(true);
        SelectedDiscValue = PlayerPrefs.GetInt("selectedDisc");
        lastButton = GetLastSelectedButton();

        if (lastButton != null)
        {
            lastButton.text = "Selected";
            lastButton.color = myColor;
        }
        else
        {
            Debug.Log("Last Button not found");
        }
    }

    public void closeDiscUI()
    {
        DiscSelectionUI.SetActive(false);
    }
}
