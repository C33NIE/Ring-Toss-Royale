using Photon.Pun;
using UnityEngine;
using TMPro;
using Photon.Realtime;
using System.Collections.Generic;

public class Launcher : MonoBehaviourPunCallbacks
{
    public static Launcher instance;

    public GameObject loadingScreen;
    public TMP_Text loadingText;
    
    public GameObject menuButtons;
    

    public GameObject creatRoomScreen;
    public TMP_InputField roomNameInput;

    public GameObject roomScreen;
    public TMP_Text roomNameText;
    public TMP_Text playerNameLabel;
    private List<TMP_Text> allPlayersNames = new List<TMP_Text>();

    public GameObject errorScreen;
    public TMP_Text errorText;

    public GameObject roomBrowserScreen;
    public RoomButton theRoomButton;
    private List<RoomButton> allRoomButtons = new List<RoomButton>();


    public GameObject nameInputScreen;
    public TMP_InputField nameInput;
    private bool hasSetNickName;

    public string levelToPlay;
    public GameObject startButton;

    public GameObject RoomTestButton;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        CloseAllMenues();
        loadingScreen.SetActive(true);
        loadingText.text = "Connecting to Server....";

        PhotonNetwork.ConnectUsingSettings();

#if UNITY_EDITOR
        RoomTestButton.SetActive(true);
#endif
    }

    public void CloseAllMenues()
    {
        menuButtons.SetActive(false);
        loadingScreen.SetActive(false);
        creatRoomScreen.SetActive(false);
        roomScreen.SetActive(false);
        errorScreen.SetActive(false);
        roomBrowserScreen.SetActive(false);
        nameInputScreen.SetActive(false);
    }

    public override void OnConnectedToMaster()
    {
        

        loadingText.text = "Joinning Lobby";
        PhotonNetwork.JoinLobby();

        PhotonNetwork.AutomaticallySyncScene = true;
    }
    public override void OnJoinedLobby()
    {
        CloseAllMenues();
        menuButtons.SetActive(true);
        PhotonNetwork.NickName = Random.Range(0, 1000).ToString();

        if (!hasSetNickName)
        {
            CloseAllMenues();
            nameInputScreen.SetActive(true);

            if (PlayerPrefs.HasKey("PlayerName"))
            {
                nameInput.text = PlayerPrefs.GetString("PlayerName");
            }
        }
        else
        {
            PhotonNetwork.NickName = PlayerPrefs.GetString("PlayerName");
        }
        
    }
    public void OpenRoomCreate()
    {
        CloseAllMenues();
        creatRoomScreen.SetActive(true);
    }

    public void CreateRoom()
    {
        if (!string.IsNullOrEmpty(roomNameInput.text))
        {
            RoomOptions roomOption = new RoomOptions();
            roomOption.MaxPlayers = 4;

            PhotonNetwork.CreateRoom(roomNameInput.text, roomOption);

            CloseAllMenues();
            loadingText.text = "Creating Room";
            loadingScreen.SetActive(true);
        }
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        errorText.text = "Failed to Create Room: " + message;
        CloseAllMenues();
        errorScreen.SetActive(true);
    }

    public void CloseErrorScreen()
    {
        CloseAllMenues();
        menuButtons.SetActive(true);
    }

    public void leaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        CloseAllMenues () ;
        loadingText.text = "Leaving Room";
        loadingScreen.SetActive(true);
    }

    public override void OnJoinedRoom()
    {
        CloseAllMenues();
        roomScreen.SetActive(true);

        roomNameText.text = PhotonNetwork.CurrentRoom.Name;

        ListAllPlayers();

        if (PhotonNetwork.IsMasterClient)
        {
            startButton.SetActive(true);
        }
        else
        {
            startButton.SetActive(false);
        }
    }

    private void ListAllPlayers()
    {
        foreach (TMP_Text player in allPlayersNames)
        {
            Destroy(player.gameObject);
        }
        allPlayersNames.Clear();

        Player[] players = PhotonNetwork.PlayerList;

        for (int i = 0; i < players.Length; i++) 
        {
            TMP_Text newPlayerLabel = Instantiate(playerNameLabel, playerNameLabel.transform.parent);
            newPlayerLabel.text = players[i].NickName;
            newPlayerLabel.gameObject.SetActive(true);

            allPlayersNames.Add(newPlayerLabel);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        TMP_Text newPlayerLabel = Instantiate(playerNameLabel, playerNameLabel.transform.parent);
        newPlayerLabel.text = newPlayer.NickName;
        newPlayerLabel.gameObject.SetActive(true);

        allPlayersNames.Add(newPlayerLabel);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        ListAllPlayers();
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        CloseAllMenues();
        loadingText.text = " Leaving Room";
        loadingScreen.SetActive(true);
    }

    public override void OnLeftRoom()
    {
        CloseAllMenues();
        menuButtons.SetActive(true);
    }

    

    public void JoinRoom(RoomInfo roomInfo)
    {
        PhotonNetwork.JoinRoom(roomInfo.Name);

        CloseAllMenues();
        loadingText.text = "Joining Room";
        loadingScreen.SetActive(true);
    }

    public void OpenBrowerScreen()
    {
        CloseAllMenues ();
        roomBrowserScreen.SetActive(true);
    }

    public void CloseBrowerScreen()
    {
        CloseAllMenues();
        menuButtons.SetActive(true);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach(RoomButton rb in allRoomButtons)
        {
            Destroy(rb.gameObject);
        }
        allRoomButtons.Clear();
        theRoomButton.gameObject.SetActive(false);

        for (int i = 0; i < roomList.Count; i++ )
        {
            if (roomList[i].PlayerCount != roomList[i].MaxPlayers && !roomList[i].RemovedFromList )
            {
                RoomButton newButton = Instantiate(theRoomButton,theRoomButton.transform.parent);
                newButton.SetButtonDetails(roomList[i]);
                newButton.gameObject.SetActive(true);

                allRoomButtons.Add(newButton);
            }
        }
    }

    public void setNickName()
    {
        if (!string.IsNullOrEmpty(nameInput.text)) 
        {
            PhotonNetwork.NickName = nameInput.text;

            PlayerPrefs.SetString("PlayerName",nameInput.text);

            CloseAllMenues ();
            menuButtons.SetActive(true);

            hasSetNickName = true;
        }
    }

    public void StartGame()
    {
        PhotonNetwork.LoadLevel(levelToPlay);
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            startButton.SetActive(true);
        }
        else
        {
            startButton.SetActive(false);
        }
    }

    public void QuickJoin()
    {
        RoomOptions roomOption = new RoomOptions();
        roomOption.MaxPlayers = 10;

        PhotonNetwork.CreateRoom("Test Room", roomOption);

        CloseAllMenues();
        loadingText.text = "Creating Room";
        loadingScreen.SetActive(true);
    }
    public void QuitGame()
    {
       Application.Quit();
    }

    public  void LeaveLobby()
    {
        PhotonNetwork.Disconnect();
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main Menu");
    }
}