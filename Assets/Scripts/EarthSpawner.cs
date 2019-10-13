using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthSpawner : ObjectSpawner
{
    [SerializeField] private GameObject _prefabBuilding;
    [SerializeField] private Sprite[] _imageBuilding;
    [SerializeField] private int _numberOneLineSpawn;
    [SerializeField] private int _offsetLinesSpawn;
    
    protected new void Start()
    {
        base.Start();
        _nextSpawnPosition = _startSpawnPoint;
        for (int i=0; i<_numberOfObjects;i++)
        {
            ComputeNextPosition(_offsetLinesSpawn, 0);
            for (int y=0; y< _numberOneLineSpawn; y++)
            {
                ComputeNextPosition(0, GetRandomXPosition(y));

                GameObject building = Instantiate(_prefabBuilding, transform);
                building.transform.position = _nextSpawnPosition;
                building.GetComponent<SpriteRenderer>().sprite = _imageBuilding[Random.Range(0, _imageBuilding.Length - 1)];
                _objectQueue.Enqueue(building);
            }
        }
    }

    private float GetRandomXPosition(int i)
    {
        float x = i*Mathf.Abs(_borderSpawn * 2) / _numberOneLineSpawn + _borderSpawn;
        //float x = Random.Range(i* section - _borderSpawn, (i+1)* section - _borderSpawn);
        Debug.Log("x position = "+x);
        return x;
    }

    protected override void OnRecycleObject(GameObject recycleObject)
    {
        recycleObject.GetComponent<SpriteRenderer>().sprite = _imageBuilding[Random.Range(0, _imageBuilding.Length - 1)];
    }

    protected override int GetLenghtQueue => _numberOfObjects*_numberOneLineSpawn;

    protected override void ComputeNextPosition(float z, float x)
    {
        _nextSpawnPosition.z += z;
        _nextSpawnPosition.x = x;
    }
}
