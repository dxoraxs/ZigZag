using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultTile : TileBlock
{
    [SerializeField] private GameObject _crystalPrefab;
    [SerializeField] private int _percentSpawnCrystal;
    private Transform _crystalTransform;

    public void RandomSpawnCrystal()
    {
        if (Random.Range(0, 100) <= _percentSpawnCrystal)
        {
            _crystalTransform = Instantiate(_crystalPrefab, transform).transform;
            _crystalTransform.position = transform.position + new Vector3(0, 1);
        }
    }
    
    protected override void OnBlockReset()
    {
        if (_crystalTransform != null)
            Destroy(_crystalTransform.gameObject);
    }
}
