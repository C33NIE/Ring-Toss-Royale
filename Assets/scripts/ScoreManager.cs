using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;

public class ScoreManager : MonoBehaviourPunCallbacks
{
    private void OnTriggerEnter(Collider other)
    {
        if (photonView.IsMine)
        {
            if (other.gameObject.tag == "Pole")
            {
                PhotonNetwork.LocalPlayer.AddScore(10);
            }
            if (other.gameObject.tag == "FinalPole")
            {
                PhotonNetwork.LocalPlayer.AddScore(20);
            }
        }
    }
}
