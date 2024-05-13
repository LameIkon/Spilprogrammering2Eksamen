using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnFactory : NetworkBehaviour
{
    //Here we put in the prefabObjects to be spawned. In the future could possible make it an array of prefabs for better sorting.
    [SerializeField] private GameObject _coinPrefab;
    [SerializeField] private GameObject _capsulePrefab;


    [SerializeField] private Collider _spawnCollider; //This is the collider which determines the area we can spawn objects in
    public int _spawnTimer; //How many seconds between objects spawned.
    public static int _objectsSpawned; //How many objects currently in the game
    public int _maxObjects; //How many objects we can maximum have before stopping spawning objects.

    private Vector3 _colliderMin, _colliderMax; //the length and width of the collider.

    private Vector3 _randomSpawn; //a vector 3 to hold where to spawn next object
    // private Coroutine _spawningObjects;

    public static SpawnFactory _Instance;

    private void Awake()
    {
        if (_Instance == null)
        {
            _Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void Start()
    {
        _colliderMin = _spawnCollider.bounds.min;
        _colliderMax = _spawnCollider.bounds.max;

        Spawning();
    }

    [ServerCallback]
    private void Spawning()
    {
        StartCoroutine(SpawningObjects(_spawnTimer));    
    }

    public GameObject CreateItem(ItemType type, Vector3 position)
    {
        GameObject spawned = null;


        switch (type)
        {
            case ItemType.Coin:
                spawned = Instantiate(_coinPrefab, position, Quaternion.identity); //If itemType is coin we spawn the coinprefab
                break;
            case ItemType.Capsule:
                spawned = Instantiate(_capsulePrefab, position, Quaternion.identity);
                break;
        }

        return spawned;

    }

    public void SpawnRandomItem(Vector3 spawnPos)
    {
        ItemType randomType = (ItemType)UnityEngine.Random.Range(0, 
            (float)Enum.GetValues(typeof(ItemType)).Cast<ItemType>().Max() + 1); //Here we take a random itemType among the enums in "ItemObject.cs".
        GameObject spawnedItem = CreateItem(randomType, spawnPos); //Here we create an object with a random Type and a random spawn withing the collider.
        NetworkServer.Spawn(spawnedItem); //make sure the object is also spawned on the server
    }

    [Server]
    private IEnumerator SpawningObjects(int time)
    {
        for (_objectsSpawned = 0; _objectsSpawned < _maxObjects; _objectsSpawned++) //As long as the ObjectsSpawned is less then maxObjects we still spawn objects.
        {
            _randomSpawn = new Vector3(UnityEngine.Random.Range(_colliderMin.x, _colliderMax.x), UnityEngine.Random.Range(_colliderMin.y, _colliderMax.y),
            UnityEngine.Random.Range(_colliderMin.z, _colliderMax.z)); //Here we determine a new spawn point

            SpawnRandomItem(_randomSpawn);
            yield return new WaitForSeconds(time);
        }
    }
}
