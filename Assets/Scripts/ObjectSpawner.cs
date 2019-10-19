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
        _objectQueue.Enqueue(OnRecycleObject(recycleObject));
        AfterAddingInQueue(recycleObject.transform.position.x);
    }

    protected virtual void AfterAddingInQueue(float lastXPosition) { }

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

    protected virtual GameObject OnRecycleObject(GameObject recycleObject)
    {
        return recycleObject;
    }

    protected void Start()
    {
        _objectQueue = new Queue<GameObject>();
    }

    protected GameObject SpawnObjectWithParent(GameObject prefab, Transform transformParent, Vector3 position, Vector3 angle = new Vector3())
    {
        GameObject spawnObject = Instantiate(prefab, transformParent);
        spawnObject.transform.localPosition = /*transformParent.localPosition +*/ position;
        if (angle != new Vector3()) spawnObject.transform.localRotation = Quaternion.Euler(angle);
        return spawnObject;
    }
}