using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TileSpawner : MonoBehaviour
{
    [Inject] private PlayerMovement _playerScript;
    [Inject] private GameController _gameController;
    [SerializeField] private Vector3 _startSpawnPoint;
    [SerializeField] private Vector3 _offsetNextPosition;
    [SerializeField] private GameObject _titlePrefab;
    [SerializeField] private int _numberOfObjects;
    [SerializeField] private float _startAnimationOffset;
    [SerializeField] private float _borderSpawn;
    private Vector3 _nextSpawnPosition;
    private Queue<GameObject> _objectQueue;

    private void Start()
    {
        _objectQueue = new Queue<GameObject>(_numberOfObjects);
        for (int i=0; i<3; i++)
        {
            _nextSpawnPosition = _startSpawnPoint;
            ComputeNextPosition(0.7f * i, 0.7f * i);
            for (int x = 0; x < 3; x++)
            {
                GameObject o = Instantiate(_titlePrefab, transform);
                o.transform.position = _nextSpawnPosition;
                ComputeNextPosition(0.7f, -0.7f);
                _objectQueue.Enqueue(o);
            }
        }
        for (int i=0; i< _numberOfObjects-9; i++)
        {
            GameObject o = Instantiate(_titlePrefab, transform);
            Recycle(o);
        }
    }

    private void FixedUpdate()
    {
        if (!_gameController.IsGame) return;
        else if (_objectQueue.Peek().transform.GetComponent<TileBlock>().GetLocalPosition + _startAnimationOffset < _playerScript.GetZPositinion())
        {
            _objectQueue.Peek().transform.GetComponent<TileBlock>().FallTileBlock();
            StartCoroutine("Fade", _objectQueue.Dequeue());
        }
    }

    IEnumerator Fade(GameObject recycleObject)
    {
        yield return new WaitForSeconds(1);
        Recycle(recycleObject);
    }

    private void Recycle(GameObject recycleObject = null)
    {
        if (recycleObject == null)
        {
            recycleObject = _objectQueue.Dequeue();
        }
        recycleObject.transform.localPosition = _nextSpawnPosition;
        recycleObject.GetComponent<TileBlock>().ResetBLock();
        recycleObject.GetComponent<TileBlock>().RandomSpawnCrystal();
        ComputeNextPosition(_offsetNextPosition.z, CorrectionXPosition());
        _objectQueue.Enqueue(recycleObject);
    }

    private void ComputeNextPosition(float z, float x)
    {
        _nextSpawnPosition.z += z;
        _nextSpawnPosition.x += x;
    }

    private float CorrectionXPosition()
    {
        if (_nextSpawnPosition.x <= -_borderSpawn) 
            return _offsetNextPosition.x;
        else if (_nextSpawnPosition.x >= _borderSpawn)
            return -_offsetNextPosition.x;
        else
            return Random.Range(0, 100) > 50 ? _offsetNextPosition.x : -_offsetNextPosition.x;
    }
}
