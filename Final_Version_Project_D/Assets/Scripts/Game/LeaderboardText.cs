using TMPro;
using UnityEngine;

public class LeaderboardText : MonoBehaviour //The purpose of this script is to fill the text of the leaderboardplayer
{
    [SerializeField] TMP_Text TextBox;

    public void Init(string name, int punten)
    {
        TextBox.text = name + " : " + punten;
    }
}
