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

    [SerializeField] private GameObject _coinPrefab;
    [SerializeField] private GameObject _capsulePrefab;
    [SerializeField] private Collider _spawnCollider;
    public int _spawnTimer;
    public static int _objectsSpawned;
    public int _maxObjects;

    private Vector3 _colliderMin, _colliderMax;

    private Vector3 _randomSpawn;
    // private Coroutine _spawningObjects;

    [SyncVar]
    public List<GameObject> _objectList;

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
                spawned = Instantiate(_coinPrefab, position, Quaternion.identity);
                break;
            case ItemType.Capsule:
                spawned = Instantiate(_capsulePrefab, position, Quaternion.identity);
                break;
        }

        return spawned;

    }

    public void SpawnRandomItem(Vector3 spawnPos)
    {
        ItemType randomType = (ItemType)UnityEngine.Random.Range(0, (float)Enum.GetValues(typeof(ItemType)).Cast<ItemType>().Max() +1);
        GameObject spawnedItem = CreateItem(randomType, spawnPos);
        //AddItem(spawnedItem);
    }

    [Command]
    private void AddItem(GameObject item) 
    {
        _objectList.Add(item);
    }

    [Server]
    private IEnumerator SpawningObjects(int time)
    {

        for (_objectsSpawned = 0; _objectsSpawned < _maxObjects; _objectsSpawned++)
        {
            _randomSpawn = new Vector3(UnityEngine.Random.Range(_colliderMin.x, _colliderMax.x), UnityEngine.Random.Range(_colliderMin.y, _colliderMax.y), UnityEngine.Random.Range(_colliderMin.z, _colliderMax.z));
            SpawnRandomItem(_randomSpawn);
            yield return new WaitForSeconds(time);
        }
    }
}
