using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ObjectSpawner : MonoBehaviour
{
    [Inject] protected PlayerMovement _playerScript;
    [Inject] protected GameController _gameController;
    [SerializeField] protected Vector3 _startSpawnPoint;
    [SerializeField] protected Vector3 _offsetNextPosition;
    [SerializeField] protected int _numberOfObjects;
    [SerializeField] protected float _borderSpawn;
    protected Queue<GameObject> _objectQueue;
    protected Vector3 _nextSpawnPosition;

    protected virtual void ComputeNextPosition(float z, float x)
    {
        _nextSpawnPosition.z += z;
        _nextSpawnPosition.x += x;
    }

    protected void Recycle(GameObject recycleObject = null)
    {
        if (recycleObject == null)
        {
            recycleObject = _objectQueue.Dequeue();
        }

        if (!IsContinue(recycleObject)) return;

        recycleObject.transform.localPosition = _nextSpawnPosition;
        ComputeNextPosition(_offsetNextPosition.z, CorrectionXPosition());
        OnRecycleObject(recycleObject);
        _objectQueue.Enqueue(recycleObject);
    }

    protected float CorrectionXPosition()
    {
        if (_nextSpawnPosition.x <= -_borderSpawn)
            return _offsetNextPosition.x;
        else if (_nextSpawnPosition.x >= _borderSpawn)
            return -_offsetNextPosition.x;
        else
            return Random.Range(0, 100) > 50 ? _offsetNextPosition.x : -_offsetNextPosition.x;
    }

    protected virtual bool IsContinue(GameObject recycleObject)
    {
        return true;
    }

    protected virtual void OnRecycleObject(GameObject recycleObject)
    {

    }

    protected virtual int GetLenghtQueue => _numberOfObjects;

    protected void Start()
    {
        _objectQueue = new Queue<GameObject>(GetLenghtQueue);
    }
}
