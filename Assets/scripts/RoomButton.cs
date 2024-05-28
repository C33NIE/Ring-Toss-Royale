using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Realtime;

public class RoomButton : MonoBehaviour
{

    public TMP_Text buttonText;
    private RoomInfo info;

    public void SetButtonDetails(RoomInfo roomInfo)
    {
        this.info = roomInfo;
        buttonText.text = roomInfo.Name;
    }
    public void OpenRoom()
    {
        Launcher.instance.JoinRoom(info);
    }
    
}
