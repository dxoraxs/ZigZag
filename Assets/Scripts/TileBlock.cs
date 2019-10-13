using UnityEngine;

public class TileBlock : MonoBehaviour
{
    private Rigidbody _rigidbody;

    public float GetLocalPosition => transform.localPosition.z;

    public void FallTileBlock()
    {
        _rigidbody.isKinematic = false;
    }

    public void ResetBLock()
    {
        _rigidbody.isKinematic = true;
        OnBlockReset();
    }

    protected virtual void OnBlockReset() {}

    private void Awake()
    {        
        if (_rigidbody==null) _rigidbody = GetComponent<Rigidbody>();
    }    
}
