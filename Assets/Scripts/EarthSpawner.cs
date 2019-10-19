using UnityEngine;
using Zenject;

public class EarthSpawner : ObjectSpawner
{
    [SerializeField] private GameObject _house;
    [SerializeField] private GameObject _rock;
    [SerializeField] private GameObject[] _trees;
    [SerializeField] private float _backgroundSpeed;
    [SerializeField] private float _offsetRecycle;
    [Inject] private PlayerMovement _targetTransform;

    protected new void Start()
    {
        base.Start();
        _nextSpawnPosition = _startSpawnPoint;
        for (int i = 0; i < _numberOfObjects; i++)
        {
            _objectQueue.Enqueue(RandomSpawnForestObject());
        }
    }

    protected override GameObject OnRecycleObject(GameObject recycleObject)
    {
        Destroy(recycleObject);
        GameObject spawnObject = RandomSpawnForestObject();

        //ComputeNextPosition(25, 0);
        return spawnObject;
        //recycleObject.GetComponent<SpriteRenderer>().sprite = _imageBuilding[Random.Range(0, _imageBuilding.Length - 1)];
        //return recycleObject;
    }

    protected override void ComputeNextPosition(float z, float x)
    {
        _nextSpawnPosition.z += z;
        _nextSpawnPosition.x = x;
    }

    private GameObject RandomSpawnForestObject()
    {
        switch (Random.Range(0, 5))
        {
            case 3:
                return VilageSpawn();
            case 4:
                return RockSpawn();
            default:
                return TreesSpawn();
        }
    }

    private GameObject RockSpawn()
    {
        GameObject rock = SpawnObjectWithParent(_rock, transform, _nextSpawnPosition, new Vector3(-90, Random.Range(0, 24) * 10, 0));
        float randomScale = Random.Range(_rock.transform.localScale.x - 5f, _rock.transform.localScale.x + 5f);
        rock.transform.localScale = new Vector3(randomScale, randomScale, randomScale);
        ComputeNextPosition(5, 0);
        return rock;
    }

    private GameObject TreesSpawn()
    {
        _nextSpawnPosition.x = Random.Range(-1, 1) * 3.5f;
        GameObject forest = SpawnParentObject(transform, "Forest Spawn");

        for (int i = 0; i < 4; i++)
        {
            GameObject trees = SpawnObjectWithParent(_trees[Random.Range(0, _trees.Length)], forest.transform, new Vector3(i % 2 == 0 ? 1.2f : -1.2f, 0, i * 1.2f), new Vector3(-90, Random.Range(0, 24) * 10, 0));
            ComputeNextPosition(2f, i % 2 == 0 ? 1f : -1f);
        }

        ComputeNextPosition(5, 0);
        return forest;
    }

    private GameObject VilageSpawn()
    {
        _nextSpawnPosition.x = Random.Range(_borderSpawn, -_borderSpawn);
        GameObject vilage = SpawnParentObject(transform, "Vilage Spawn");

        for (int i = 0; i < 2; i++)
        {
            GameObject houseFirst = SpawnObjectWithParent(_house, vilage.transform, new Vector3(0, 0, i * 3f), new Vector3(-90, -90, 0));

            GameObject houseSecond = SpawnObjectWithParent(_house, houseFirst.transform, new Vector3(0, 0.12f), new Vector3(0, 0, 180));
            houseSecond.transform.localScale = Vector3.one;

            ComputeNextPosition(2f, 0);
        }
        Quaternion randomRotationVilage = Quaternion.Euler(new Vector3(0, Random.Range(-5, 5) * 10, 0));
        vilage.transform.rotation = randomRotationVilage;

        ComputeNextPosition(5, 0);
        return vilage;
    }

    private GameObject SpawnParentObject(Transform parent, string nameObject = "object")
    {
        GameObject parentObject = new GameObject();
        parentObject.transform.position = parent.transform.position + _nextSpawnPosition;
        parentObject.transform.parent = parent;
        parentObject.transform.name = nameObject;
        return parentObject;
    }

    private void Update()
    {
        //if (!_gameController.IsGame) return;

        transform.position = new Vector3(transform.position.x, transform.position.y, _targetTransform.GetZPositinion() * _backgroundSpeed);

        if (_objectQueue.Peek().transform.position.z + _offsetRecycle < _playerScript.GetZPositinion())
        {
            Recycle();
        }
    }
}