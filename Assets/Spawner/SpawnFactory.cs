using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFactory : MonoBehaviour
{

    [SerializeField] private GameObject _coinPrefab;
   
    public GameObject CreateItem(ItemType type, Vector3 position)
    {
        GameObject spawned = null;
        

        switch (type)
        {
            case ItemType.Coin:
                spawned = Instantiate(_coinPrefab, position, Quaternion.identity);
                break;          
        }

        return spawned;
        
    }

    public void SpawnRandomItem(Vector3 spawnPos)
    {
       // ItemType randomType = (ItemType)Random.Range(0, 1);
        GameObject spawnedItem = CreateItem(ItemType.Coin, spawnPos);
    }
}
