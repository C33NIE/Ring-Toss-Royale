using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Photon.Pun;
using TMPro;
using Photon.Pun.UtilityScripts;

public class Leaderboard : MonoBehaviour
{
    public GameObject PlayersHolder;

    [Header("Options")]
    public float refreshRate = 1.0f;

    [Header("UI")]
    public GameObject[] slots;
    public GameObject[] slotsFinal;

    [Space]
    public TextMeshProUGUI[] scoreText;
    public TextMeshProUGUI[] nameText;

    public TextMeshProUGUI[] scoreTextFinal;
    public TextMeshProUGUI[] nameTextFinal;

    public TMPro.TextMeshProUGUI winner; 

    private void Start()
    {
        InvokeRepeating(nameof(Refresh), 1f, refreshRate);
    }

    public void Refresh()
    {
        foreach (var slot in slots) 
        {
            slot.SetActive(false);
        }
        foreach (var slotFinal in slotsFinal)
        {
            slotFinal.SetActive(false);
        }

        var sortedPlayerList = (from player in PhotonNetwork.PlayerList orderby player.GetScore() descending select player).ToList();

        int i = 0;

        foreach (var player in sortedPlayerList)
        {
            slots[i].SetActive(true);
            slotsFinal[i].SetActive(true);

            if (player.NickName == "")
            {
                player.NickName = "unnamed";
            }

            nameText[i].text = player.NickName;
            nameTextFinal[i].text = player.NickName;



            scoreText[i].text = player.GetScore().ToString();
            scoreTextFinal[i].text = player.GetScore().ToString();

            i++;

        }
        winner.text = nameText[0].text + " Won!";
    }

    private void Update()
    {
        if (FinalLevel.Won == true)
        {
            PlayersHolder .SetActive(false);
        }
    }
}
