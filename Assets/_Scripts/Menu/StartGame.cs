using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    void Start()
    {
        if (MainMenu.isServer)
        {
            NetworkManager.singleton.StartHost();
        }
        else
        {
            NetworkManager.singleton.StartClient();
        }
    }
}
