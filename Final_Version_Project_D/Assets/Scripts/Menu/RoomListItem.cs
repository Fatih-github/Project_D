using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using TMPro;


public class RoomListItem : MonoBehaviourPunCallbacks
{
    [SerializeField] TMP_Text _text;
    public RoomInfo info;

    public void SetRoomInfo(RoomInfo roominfo) 
    {
        this.info = roominfo;
        _text.text = roominfo.Name; 
    }
    public void OnClick()
    {
        Launcher.Instance.JoinRoom(info);
    }
}
