using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerSpawner : MonoBehaviour
{
    Player[] PlayersInGame;
    public static PlayerSpawner Instance;
    int MyNumberInRoom;

    public GameObject playerPrefab;


    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        PlayersInGame = PhotonNetwork.PlayerList;
        for (int i = 0;i<PlayersInGame.Length;i++)
        {
            if (PlayersInGame[i] == PhotonNetwork.LocalPlayer)
            {
                MyNumberInRoom = i;
            }
            Debug.Log("Players in Game:" + PlayersInGame.Length);
        }

        if (PhotonNetwork.IsConnected) 
        {
            SpawnPlayer();
        }
    }

    public void SpawnPlayer()
    {
        
        Transform SpawnPoint = SpawnManager.Instance.GetSpawnPoint(MyNumberInRoom);

        GameObject MyPlayer = PhotonNetwork.Instantiate(playerPrefab.name, SpawnPoint.position, SpawnPoint.rotation);

        MyPlayer.transform.Find("Main Camera").gameObject.SetActive(true);
    }


}
