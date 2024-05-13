using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGame : MonoBehaviour
{
    //exit the game
    public void ExitButton()
    {
        // only quits the editor if its the unity editor application
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif

        //when the game is not in the unity editor application quit with this method
        Application.Quit();

    }
}
