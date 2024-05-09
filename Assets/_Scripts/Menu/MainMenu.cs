using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    static public bool isServer = true;

    public void PlayGameAsServer()
    {
        SceneManager.LoadScene("MainScene");
    }
    public void PlayGameAsClient()
    {
        isServer = false;
        SceneManager.LoadScene("MainScene");
    }
}
