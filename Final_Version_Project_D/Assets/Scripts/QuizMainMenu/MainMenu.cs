using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
public class MainMenu : MonoBehaviour
{
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(3);
    }

    public void GoBack()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel(0);
        //SceneManager.LoadScene(0);
    }
}
