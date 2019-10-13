using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawner : ObjectSpawner
{
    [SerializeField] private GameObject _titlePrefab;
    [SerializeField] private GameObject _titlePrefabWithPortal;
    [SerializeField] private float _startAnimationOffset;

    private int _blockToNextEvent;

    protected new void Start()
    {
        base.Start();
        _blockToNextEvent = Random.Range(10, 20);
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
        
        if (_objectQueue.Peek().transform.GetComponent<TileBlock>().GetLocalPosition + _startAnimationOffset < _playerScript.GetZPositinion())
        {
            _objectQueue.Peek().transform.GetComponent<TileBlock>().FallTileBlock();
            StartCoroutine("Fade", _objectQueue.Dequeue());
        }
    }

    private IEnumerator Fade(GameObject recycleObject)
    {
        yield return new WaitForSeconds(1);
        Recycle(recycleObject);
    }

    protected override void OnRecycleObject(GameObject recycleObject)
    {
        recycleObject.GetComponent<TileBlock>().ResetBLock();
        recycleObject.GetComponent<DefaultTile>().RandomSpawnCrystal();

        _blockToNextEvent--;
        if (_blockToNextEvent==0)
        {
            SpawPortal(recycleObject.transform.position.x);
        }
        recycleObject.transform.rotation = Quaternion.Euler(new Vector3(0, Random.Range(0, 100) < 50 ? 45 : -45, 0));
    }

    protected override bool IsContinue(GameObject recycleObject)
    {
        if (!recycleObject.GetComponent<DefaultTile>())
        {
            Destroy(recycleObject);
            return false;
        }
        return true;
    }

    private void SpawPortal(float lastPosition)
    {
        GameObject portaEntryl = Instantiate(_titlePrefabWithPortal, transform);
        _objectQueue.Enqueue(portaEntryl);
        portaEntryl.GetComponent<PortalTile>().SetPortalTypeEntry();
        portaEntryl.transform.position = _nextSpawnPosition;
        ComputeNextPosition(0.7f, 0.7f * (int)Random.Range(4, 6) * (Random.Range(0, 10) < 5 ? -1 : 1));
        _blockToNextEvent = Random.Range(10, 20);
        if (portaEntryl.transform.position.x > lastPosition)
        {
            portaEntryl.transform.rotation = Quaternion.Euler(new Vector3(0, -45, 0));
        }

        GameObject portaExit = Instantiate(_titlePrefabWithPortal, transform);
        _objectQueue.Enqueue(portaExit);
        portaExit.transform.position = _nextSpawnPosition;

        portaEntryl.GetComponent<PortalTile>().SetExitPortal(portaExit.transform);
        ComputeNextPosition(0.7f, -0.7f);
    }
}
