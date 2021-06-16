using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomProperty;
using Photon.Pun;
using Photon.Realtime;

public class FinalTask : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMesh _textobject;
    public string[] _tasks;
    // Start is called before the first frame update
    void Start()
    {
        randomize();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void randomize()
    {
        _textobject.text = _tasks[Random.Range(0, _tasks.Length)];
    }

    public void OnTriggerStay(Collider other)
    {
        Debug.Log("Interact");
        if (Input.GetMouseButton(1))
        {
            Debug.Log("F key was pressed.");
            StartCoroutine(ClickedMonitor());
        }
    }
    private IEnumerator ClickedMonitor()
    {
        addPunten(10);
        randomize();
        yield return new WaitForSeconds(2);
    }

    private void addPunten(int punten)
    {
        CustomProperties CP = new CustomProperties();
        Player player = PhotonNetwork.LocalPlayer;
        int currScore = CP.GetScore(player);
        CP.SetScore(player, currScore + punten);
    }



}
