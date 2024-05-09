using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class SpawnFactory : MonoBehaviour
{

    [SerializeField] private GameObject _coinPrefab;
    [SerializeField] private Collider _spawnCollider;
    public int _coinTimer;
    public static int _objectsSpawned;
    public int _maxObjects;

    private Vector3  _colliderMin, _colliderMax;

    private Vector3 _randomSpawn;
   // private Coroutine _spawningObjects;


    public void Start()
    {
        _colliderMin = _spawnCollider.bounds.min;
        _colliderMax = _spawnCollider.bounds.max;

        Spawning();
    }

    private void Spawning()
    {
      StartCoroutine(SpawningObjects(_coinTimer));
    }

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


    private IEnumerator SpawningObjects(int time)
    {

        for (_objectsSpawned = 0; _objectsSpawned < _maxObjects; _objectsSpawned++)
        {
            _randomSpawn = new Vector3(Random.Range(_colliderMin.x, _colliderMax.x), Random.Range(_colliderMin.y, _colliderMax.y), Random.Range(_colliderMin.z, _colliderMax.z));
            SpawnRandomItem(_randomSpawn);
            yield return new WaitForSeconds(time);
        }
    }
}
