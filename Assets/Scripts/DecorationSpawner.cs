using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class DecorationSpawner : MonoBehaviour
{
    [Inject] private PlayerMovement _player;
    [SerializeField] private float _borderX;
    [SerializeField] private float _borderZTop;
    [SerializeField] private float _borderZBot;
    [SerializeField] private float _spawnYPosition;
    [SerializeField] private GameObject[] _decorationPrefab;
    private int _blockToNextEvent;
    private Transform _spawnedObject;

    private void Start()
    {
        _blockToNextEvent = GetRandomZPosition;
    }

    private int GetRandomZPosition => (int)_player.GetZPositinion() + Random.Range(20, 40);

    private void Update()
    {
        if (_spawnedObject != null)
        {
            if (Vector3.Distance(_spawnedObject.position, new Vector3(_player.GetXPositinion(),0, _player.GetZPositinion())) > 20f)
            {
                Destroy(_spawnedObject.gameObject);
            }
        }
        else if (_player.GetZPositinion() > _blockToNextEvent)
        {
            SpawnDecoration();
            _blockToNextEvent = GetRandomZPosition;
        }
    }

    private void SpawnDecoration()
    {
        _spawnedObject = Instantiate(_decorationPrefab[Random.Range(0, _decorationPrefab.Length-1)], transform).transform;
        Vector3 spawnPosition;// = new Vector3(0, _spawnYPosition);
        Vector2 randomDirection = Random.insideUnitCircle;
        if (randomDirection.x<0)
        {
            if (randomDirection.y<0) spawnPosition = new Vector3(_borderX +1, Random.value > 0.5f ? _spawnYPosition : 2, _borderZTop +1);
            else spawnPosition = new Vector3(_borderX + 1, _spawnYPosition, _borderZBot - 1);
        }
        else
        {
            if (randomDirection.y < 0) spawnPosition = new Vector3(-_borderX - 1, _spawnYPosition, -_borderZTop + 1);
            else spawnPosition = new Vector3(-_borderX - 1, _spawnYPosition, _borderZBot - 1);
        }
        spawnPosition = new Vector3(spawnPosition.x + _player.GetXPositinion(), spawnPosition.y, spawnPosition.z + _player.GetZPositinion());
        _spawnedObject.transform.position = spawnPosition;
        _spawnedObject.GetComponent<DecorationMovement>().SetVectorMovement(randomDirection);

        //_spawnedObject.transform.position = 
    }
}
