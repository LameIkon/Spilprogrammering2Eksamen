using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFactory : NetworkBehaviour
{
    private int _value;
    private string _name;
    private ItemType _itemType;


    [SerializeField] private ItemObject _item; //This where we determine what item the ItemFactory is going to "produce"


    private void Awake()
    {
        // Here we load the properties from the scriptable ItemObject "on to" the prefabObject.
        _value = _item._Value;
        _name = _item._Name;
        _itemType = _item._Type;
    }

    [ServerCallback]
    public virtual void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerMovement>().CommandSetPlayerScore(_value); //apllies the value on the object to the score.
            SpawnFactory._objectsSpawned--; //Deincriment how many objects are in the game.
            Destroy(gameObject);
        }
    }

}
