using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.IO;
using CustomProperty;

public class PlayerManager : MonoBehaviourPunCallbacks
{
    PhotonView PV;
    public GameObject Player;
    private Player LocalPlayer;
    public string PlayerName;
    public GameObject Customization; //mogelijke kleding en cosmetic
    
    private int _punten; //reserve
    public int Punten 
        
    {
        get { return _punten; }
        set 
        {
            if(_punten != value)
            {
                _punten = value;
                PV.RPC("SynchScore", RpcTarget.All, value);
            }
        }
    }

    
    PlayerManager pm;
    private void Awake()
    {
        this.PV = GetComponent<PhotonView>();
        Player = this.gameObject;

    }


    public void Start()
    {
        if (PV.IsMine)
        {
            LocalPlayer = PhotonNetwork.LocalPlayer;
            PlayerName = LocalPlayer.NickName;
            var cp = new CustomProperties();
            int punten = cp.GetScore(LocalPlayer); 
            PV.RPC("InitStats", RpcTarget.All, punten, this.PlayerName);
            Destroy(Customization); //Destroys customization for your local player
        }
    }

    [PunRPC]
    public void InitStats(int punten, string name) //Initialize player manager stats of caller
    {
        this.Punten = punten;
        this.PlayerName = name;
    }
    [PunRPC]
    public void SynchScore(int punten) // update player manager score
    {
        this.Punten = punten;
    }
    public override void OnPlayerEnteredRoom(Player newPlayer) // send my data to the new player so he can instantiate my playermanager stats
    {
        PV.RPC("InitStats", newPlayer, this.Punten, this.PlayerName);
    }
}
