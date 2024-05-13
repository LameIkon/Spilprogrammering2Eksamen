using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    public TextMeshProUGUI playerScore;

    public void DisplayPlayerScore(int newPlayerScore)
    {
        playerScore.text = "Score: " + newPlayerScore;
    }
}
