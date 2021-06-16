using UnityEngine;
using TMPro;
using Photon.Pun;
public class Login : MonoBehaviour
{
    public void Inloggen(TMP_Text username)
    {
        if (PhotonNetwork.InRoom)
        {
            PhotonNetwork.LeaveRoom();
        }
        //hier inloggen

        Launcher l = GetComponentInParent<Launcher>();
        MenuManager m = GetComponentInParent<MenuManager>();
        m.OpenMenu("loading");
        l.Connect();
        
    }
}
