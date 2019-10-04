using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField] private float _offsetFromTarget;
    [SerializeField] private Transform _targetTransform;

    private void Update()
    {
        transform.position = new Vector3(_targetTransform.position.x, transform.position.y, _targetTransform.position.z + _offsetFromTarget);
    }
}