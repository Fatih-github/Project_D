using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using CustomProperty;
using System.Linq;

public class SettingsMenu : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject Player;
    private PhotonView PV;
    [SerializeField] TMP_Text PuntenText;
    [SerializeField] TMP_Text NameText;

    //leaderboard
    [SerializeField] Transform Content;
    [SerializeField] GameObject leaderboardText;
    // Start is called before the first frame update
    void Start()
    {
        PV = Player.GetComponent<PhotonView>();
    }

    public override void OnEnable()
    {
        PlayerManager pm = Player.GetComponent<PlayerManager>();
        PuntenText.text = "Punten = " + pm.Punten.ToString();
        NameText.text = "Name = " + pm.PlayerName;
        Leaderboard();

    }
    public override void OnDisable()
    {
        foreach(Transform child in Content)
        {
            Destroy(child.gameObject);
        }
    }

    public void Exit()
    {
        PhotonNetwork.DestroyPlayerObjects(PhotonNetwork.LocalPlayer);;
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel(0);
    }

    private void Leaderboard() //fill leaderboard content
    {
        Dictionary<int,Player> d = PhotonNetwork.CurrentRoom.Players;
        CustomProperties CP = new CustomProperties();
        Dictionary<string, int> sortplayers = new Dictionary<string, int>(); // dict to sort playerpunten from high to low
        foreach(KeyValuePair<int,Player> a in d)
        {
            Player cur = a.Value;
            int punten = (int)CP.GetScore(cur);
            sortplayers.Add(cur.NickName, punten);
        }
        var mySortedList = sortplayers.OrderBy(i => i.Value).ToList();
        mySortedList.Reverse(); // if you don't do this the scoreboard is from low to high, because every new instantiate starts above
        foreach(KeyValuePair<string,int> pair in mySortedList)
        {
           Instantiate(leaderboardText, Content).GetComponent<LeaderboardText>().Init(pair.Key, pair.Value);
        }
    }   
}
