using UnityEngine;

public class TileBlock : MonoBehaviour
{
    [SerializeField] private GameObject _crystalPrefab;
    [SerializeField] private int _percentSpawnCrystal;
    private Rigidbody _rigidbody;
    private Transform _crystalTransform;

    public float GetLocalPosition => transform.localPosition.z;

    public void FallTileBlock()
    {
        _rigidbody.isKinematic = false;
    }

    public void ResetBLock()
    {
        _rigidbody.isKinematic = true;
        if (_crystalTransform != null)
            Destroy(_crystalTransform.gameObject);
    }

    public void RandomSpawnCrystal()
    {
        if (Random.Range(0, 100) <= _percentSpawnCrystal)
        {
            _crystalTransform = Instantiate(_crystalPrefab, transform).transform;
            _crystalTransform.position = transform.position + new Vector3(0, 1);
        }
    }

    private void Awake()
    {        
        if (_rigidbody==null) _rigidbody = GetComponent<Rigidbody>();
    }    
}
