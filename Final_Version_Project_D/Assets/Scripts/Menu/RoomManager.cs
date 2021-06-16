using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using System.IO;

public class RoomManager : MonoBehaviourPunCallbacks //RoomManager controls the initializing of players;
{
    private static RoomManager _instance;
    public static RoomManager Instance { get { return _instance; } } //Singleton There can only be 1 roomManager

    private void Awake()
    {
        if (_instance != null && _instance != this) 
        {
            Destroy(this.gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        _instance = this;
    }
   
    public override void OnEnable()
    {
        base.OnEnable();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (scene.buildIndex == 1)//game scene
        {
            Vector3 coordinaten = new Vector3(-119.2f, 4f, -43.7f);
            PhotonNetwork.Instantiate(Path.Combine("PhotonPreFabs", "Player"), coordinaten, Quaternion.identity); //Maakt player op Vector(position) 0
        }
        else if(scene.buildIndex == 0)
        {
            Debug.Log("1");
        }
    }
}
