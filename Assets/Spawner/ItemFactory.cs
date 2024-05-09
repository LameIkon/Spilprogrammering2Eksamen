using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFactory : MonoBehaviour
{
    private int _value;
    private string _name;
    private ItemType _itemType;

    [SerializeField] private ItemObject _item; //This where we determine what item the ItemFactory is going to "produce"

    private void Awake()
    {        
        _value = _item._Value;
        _name = _item._Name;
        _itemType = _item._Type;
    }

    public virtual void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            //Setup for value on item being applied to a score
            SpawnFactory._objectsSpawned--;
            Destroy(gameObject);
            
        }
    }

}
