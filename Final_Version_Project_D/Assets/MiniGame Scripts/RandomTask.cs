using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CustomProperty;
using Photon.Pun;
using Photon.Realtime;
public class RandomTask : MonoBehaviour
{

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
        transform.position = newPos();
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
        addPunten(5);
        transform.position = newPos();
        randomize();
        yield return new WaitForSeconds(2);
    }
    public Vector3 newPos()
    {
        var curr = transform.position;
        float x = Random.Range(0, 4);
        Debug.Log($"plus {curr.x + x}");
        Debug.Log($"min {curr.x - x}");
        if (curr.x + x >= -53f && curr.x + x <= -43f)
        {
            return new Vector3(curr.x + x, curr.y, curr.z);
        }
        else if (curr.x - x <= -43 && curr.x - x >= -53)
        {
            return new Vector3(curr.x - x, curr.y, curr.z);
        }
        else
        {
            return curr;
        }
    }
    private void addPunten(int punten)
    {
        CustomProperties CP = new CustomProperties();
        Player player = PhotonNetwork.LocalPlayer;
        int currScore = CP.GetScore(player);
        CP.SetScore(player, currScore + punten);
    }


}
