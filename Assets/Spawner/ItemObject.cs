using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Item Object", menuName = "ItemObject")]
public class ItemObject : ScriptableObject
{
    public int _Value;
    public string _Name;
    public ItemType _Type;
}

public enum ItemType
{
    Coin
}
