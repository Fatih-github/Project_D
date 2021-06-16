using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

//  Use this class on your code to change player punten.
//  Do this by adding "using CustomProperty;" on your file and making a new object by writing "CustomProperties varname = new CustomProperties();"
//  Next you can call the functions by doing varname.GetScore/SetScore
//  The parameter target is the PhotonNetwork.Player of the character. You can get this of your current player by writing PhotonNetwork.LocalPlayer;
namespace CustomProperty
{
    public class CustomProperties //Use this class on your code to change player punten.
    {
        public int GetScore(Player target)
        {
            int res = (int)target.CustomProperties["Punten"];
            return res;
        }
        public void SetScore(Player target, int newScore)
        {
            ExitGames.Client.Photon.Hashtable temp = target.CustomProperties;
            temp["Punten"] = newScore;
            target.SetCustomProperties(temp);
        }
    }
}
