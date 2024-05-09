using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerName : MonoBehaviour
{
    public static string _Name;


    public static void SetName(string name)
    {
        _Name = name;
    }

    public static string GetLocalName()
    {
        return _Name;
    }

}
