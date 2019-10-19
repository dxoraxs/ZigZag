using System.Collections;
using UnityEngine;

public class TileSpawner : ObjectSpawner
{
    [SerializeField] private GameObject _titlePrefab;
    [SerializeField] private GameObject _titlePrefabWithPortal;
    [SerializeField] private GameObject _titleLiftPrefab;
    [SerializeField] private float _startAnimationOffset;
    [SerializeField] private BranchingCoordinate[] _branching;

    private int _blockToNextEvent;

    protected new void Start()
    {
        base.Start();
        _blockToNextEvent = Random.Range(10, 20);
        for (int i = 0; i < 3; i++)
        {
            _nextSpawnPosition = _startSpawnPoint;
            ComputeNextPosition(_offsetNextPosition.x * i, _offsetNextPosition.z * i);
            for (int x = 0; x < 3; x++)
            {
                GameObject o = SpawnObjectWithParent(_titlePrefab, transform, _nextSpawnPosition);
                ComputeNextPosition(_offsetNextPosition.z, -_offsetNextPosition.x);
                _objectQueue.Enqueue(o);
            }
        }
        for (int i = 0; i < _numberOfObjects - 9; i++)
        {
            Recycle(Instantiate(_titlePrefab, transform));
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

    protected override GameObject OnRecycleObject(GameObject recycleObject)
    {
        recycleObject.transform.localPosition = _nextSpawnPosition;
        recycleObject.GetComponent<TileBlock>().ResetBLock();
        recycleObject.GetComponent<DefaultTile>().RandomSpawnCrystal();

        ComputeNextPosition(_offsetNextPosition.z, CorrectionXPosition());
        recycleObject.transform.rotation = Quaternion.Euler(new Vector3(0, Random.value > 0.5f ? 45 : -45, 0));
        return recycleObject;
    }

    protected override void AfterAddingInQueue(float lastXPosition)
    {
        _blockToNextEvent--;
        if (_blockToNextEvent == 0)
        {
            if (Random.value > 0.5f)
            {
                SpawPortal(lastXPosition);
            }
            else
            {
                SpawnBranching(lastXPosition);
            }
            _blockToNextEvent = Random.Range(15, 25);
        }
    }

    protected override bool IsContinue(GameObject recycleObject)
    {
        if (!recycleObject.GetComponent<DefaultTile>() || _objectQueue.Count > _numberOfObjects)
        {
            Destroy(recycleObject);
            return false;
        }
        return true;
    }

    private void SpawPortal(float lastPosition)
    {

        GameObject portaEntryl = SpawnObjectWithParent(_titlePrefabWithPortal, transform, _nextSpawnPosition);
        portaEntryl.GetComponent<PortalTile>().SetPortalTypeEntry();

        _objectQueue.Enqueue(portaEntryl);
        ComputeNextPosition(_offsetNextPosition.z, _offsetNextPosition.z * (int)Random.Range(4, 6) * (Random.Range(0, 10) < 5 ? -1 : 1));

        GameObject portaExit = SpawnObjectWithParent(_titlePrefabWithPortal, transform, _nextSpawnPosition);
        _objectQueue.Enqueue(portaExit);

        if (portaEntryl.transform.position.x > lastPosition)
            portaEntryl.transform.rotation = Quaternion.Euler(new Vector3(0, -45, 0));

        if (Random.value > 0.5f)
        {
            ComputeNextPosition(0.7f, -0.7f);
            SpawnObjectWithParent(_titlePrefab, portaExit.transform, new Vector3(-1, 0, 0)).transform.rotation = _titlePrefab.transform.rotation;
        }
        else
        {
            portaExit.transform.rotation = Quaternion.Euler(new Vector3(0, -45, 0));
            ComputeNextPosition(0.7f, 0.7f);
            SpawnObjectWithParent(_titlePrefab, portaExit.transform, new Vector3(1, 0, 0)).transform.rotation = _titlePrefab.transform.rotation;
        }

        portaEntryl.GetComponent<PortalTile>().SetExitPortal(portaExit.transform);
        //ComputeNextPosition(0.7f, 0.7f);
        ComputeNextPosition(_offsetNextPosition.z, CorrectionXPosition());
    }

    private void SpawnBranching(float lastPosition)
    {
        BranchingCoordinate selectBranch = _branching[Random.Range(0, _branching.Length)];

        for (int i = 0; i < selectBranch.position.Length; i++)
        {
            GameObject tile;
            if (selectBranch.position[i].TypeTile == TilePrefab.Portal)
            {
                GameObject firstInPortal = SpawnObjectWithParent(_titlePrefabWithPortal, transform, selectBranch.position[i].TileSpawnPosition + _nextSpawnPosition,
                    GetRotationTilePortal(selectBranch.position[i - 2].TileSpawnPosition.x, selectBranch.position[i].TileSpawnPosition.x));
                _objectQueue.Enqueue(firstInPortal);
                GameObject secondInPortal = SpawnObjectWithParent(_titlePrefabWithPortal, transform, selectBranch.position[++i].TileSpawnPosition + _nextSpawnPosition,
                    GetRotationTilePortal(selectBranch.position[i - 2].TileSpawnPosition.x, selectBranch.position[i].TileSpawnPosition.x));
                _objectQueue.Enqueue(secondInPortal);
                GameObject firstOutPortal = SpawnObjectWithParent(_titlePrefabWithPortal, transform, selectBranch.position[++i].TileSpawnPosition + _nextSpawnPosition,
                    GetRotationTilePortal(selectBranch.position[i].TileSpawnPosition.x, selectBranch.position[i + 2].TileSpawnPosition.x));
                _objectQueue.Enqueue(firstOutPortal);
                GameObject secondOutPortal = SpawnObjectWithParent(_titlePrefabWithPortal, transform, selectBranch.position[++i].TileSpawnPosition + _nextSpawnPosition,
                    GetRotationTilePortal(selectBranch.position[i].TileSpawnPosition.x, selectBranch.position[i + 2].TileSpawnPosition.x));
                _objectQueue.Enqueue(secondOutPortal);

                PortalBranch(firstInPortal, firstOutPortal);
                PortalBranch(secondInPortal, secondOutPortal);

            }
            else if (selectBranch.position[i].TypeTile == TilePrefab.Stairs)
            {
                tile = SpawnLift(_nextSpawnPosition.x + selectBranch.position[i-2].TileSpawnPosition.x, selectBranch.position[i].TileSpawnPosition + _nextSpawnPosition);
                _objectQueue.Enqueue(tile);
            }
            else
            {
                tile = SpawnObjectWithParent(_titlePrefab, transform, selectBranch.position[i].TileSpawnPosition + _nextSpawnPosition);
                tile.GetComponent<DefaultTile>().RandomSpawnCrystal();
                _objectQueue.Enqueue(tile);
            }
            
        }
        
        ComputeNextPosition(selectBranch.position[selectBranch.position.Length-1].TileSpawnPosition.z + _offsetNextPosition.z, selectBranch.position[selectBranch.position.Length - 1].TileSpawnPosition.x +_offsetNextPosition.x);
    }

    private Vector3 GetRotationTilePortal(float front, float further)
    {
        return front > further ? new Vector3(0, 45, 0) : new Vector3(0, -45, 0);
    }

    private void PortalBranch(GameObject inPortal, GameObject outPortal)
    {
        inPortal.GetComponent<PortalTile>().SetPortalTypeEntry();
        inPortal.GetComponent<PortalTile>().SetExitPortal(outPortal.transform);
    }

    private GameObject SpawnLift(float lastPosition, Vector3 spawnPosition)
    {
        GameObject lift = Instantiate(_titleLiftPrefab, transform);
        lift.transform.position = spawnPosition;

        if (lift.transform.position.x > lastPosition)
            lift.transform.rotation = Quaternion.Euler(new Vector3(0, 135, 0));
        return lift;
    }
}