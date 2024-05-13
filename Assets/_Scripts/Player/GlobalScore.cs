using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using TMPro;
using UnityEngine;

public class GlobalScore : NetworkBehaviour
{
    public TextMeshProUGUI globalScore;

    [SyncVar(hook = nameof(DisplayGlobalScore))]
    public int globalScoreValue = 0;

    public void DisplayGlobalScore(int oldScore, int newScore)
    {
        globalScore.text = "Global score: " + newScore;
    }

    private void Start()
    {
        globalScore.text = "Global score: 0";
    }
}
