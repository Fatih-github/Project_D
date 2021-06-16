using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;

public class Launcher : MonoBehaviourPunCallbacks
{
    public static Launcher Instance;

    [SerializeField] TMP_InputField roomNameInputField;
    [SerializeField] TMP_InputField playerNameInputField;
    [SerializeField] TMP_Text error_text;
    [SerializeField] TMP_Text roomNameText;
    //List of rooms
    [SerializeField] GameObject roomListPreFab;
    [SerializeField] Transform roomListContent;

    [SerializeField] GameObject GameManagerPreFab;

    private void Awake()
    {
        Instance = this; 
    }
    public void Connect() //Connect to Photon
    {
        Debug.Log("Welcome");
        ExitGames.Client.Photon.Hashtable custom = new ExitGames.Client.Photon.Hashtable();
        custom["Punten"] = 0;
        PhotonNetwork.LocalPlayer.CustomProperties = custom;
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnConnectedToMaster() //Connected to main server
    {
        Debug.Log("Connected to master");
        //instantiate roommanager

        if (GameObject.Find("RoomManager") == null)
        {
            GameObject gm = Instantiate(GameManagerPreFab, Vector3.zero, Quaternion.identity);
            gm.name = "RoomManager";
        }

        PhotonNetwork.JoinLobby();
        if (PhotonNetwork.NickName == "")
        {
            PhotonNetwork.NickName = playerNameInputField.text;
        }
    }

    public override void OnJoinedLobby() //Go to titlemenu
    {
        MenuManager.Instance.OpenMenu("title");
        Debug.Log("You JOINED");
    }
    public void CreateRoom()
    {
        if(string.IsNullOrEmpty(roomNameInputField.text))
        {
            return;
        }
        PhotonNetwork.CreateRoom(roomNameInputField.text);
        MenuManager.Instance.OpenMenu("loading");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("MY NAME IS = " + PhotonNetwork.NickName);
        StartGame();
    }


    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        error_text.text = "Error: " + message;
        MenuManager.Instance.OpenMenu("error");
        
    }
    
    public void JoinRoom(RoomInfo info)
    {
        PhotonNetwork.JoinRoom(info.Name);
        MenuManager.Instance.OpenMenu("loading");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        //Empty list with each update
        foreach (Transform trans in roomListContent)
        {
            Destroy(trans.gameObject);
        }
        //Voeg alle roomListPreFab in de lijst met elke update
        foreach (RoomInfo room in roomList) 
        {
            if (room.RemovedFromList)
            {
                continue;
            }
            Instantiate(roomListPreFab, roomListContent).GetComponent<RoomListItem>().SetRoomInfo(room);
        }
    }

   
    public void StartGame()
    {
        PhotonNetwork.LoadLevel(1);
    }
}
