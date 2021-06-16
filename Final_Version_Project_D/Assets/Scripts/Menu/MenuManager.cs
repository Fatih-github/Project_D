using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : Menu
{
    public static MenuManager Instance;

    [SerializeField] private Menu[] menuArray;

    private void Awake()
    {
        Instance = this;
    }
    public void OpenMenu(string menuName) //Open menu and close others
    {
        foreach(Menu m in menuArray)
        {
            if(m.menuName == menuName) 
            {
                m.Open();
            }
            else if (m.open)
            {
                CloseMenu(m);
            }
        }
    }
    public void OpenMenu(Menu menu) //Open menu and close others
    {
        foreach(Menu m in menuArray) 
        {
            if (m.open) 
            {
                CloseMenu(m);
            }
        }
        menu.Open();
    }
    public void CloseMenu(Menu menu)
    {
        menu.Close();
    }
}
