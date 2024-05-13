using UnityEngine;

public class PlayerName : MonoBehaviour
{
    // This GameObject is currently on an object that cant be destroyed on scene load

    public static string _Name;


    public static void SetName(string name) // Input field. When finished writing, store name here. 
    {
        _Name = name;
        Debug.Log(name);
    }

    public static string GetLocalName() // Method that gets called when player gets instantiated
    {
        return _Name;
    }

}
