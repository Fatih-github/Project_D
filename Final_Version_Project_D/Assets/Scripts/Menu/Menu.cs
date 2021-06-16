using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public string menuName;
    public bool open;
    public void Open() 
    {
        open = true;
        gameObject.SetActive(true);
    }
    public void Close()
    {
        open = false;
        gameObject.SetActive(false);
    }

    //buttons
    public void PressedCreate() 
    {
        Debug.Log("Create");
        Close();
    }
    public void PressedQuit() 
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
