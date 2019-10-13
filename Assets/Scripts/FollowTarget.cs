using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField] private float _offsetFromTarget;
    [SerializeField] private float _speedMovement;
    [SerializeField] private Transform _targetTransform;

    private void Update()
    {
        transform.position = new Vector3(
            Mathf.Lerp(transform.position.x, _targetTransform.position.x, _speedMovement * Time.deltaTime),
            transform.position.y,
            _targetTransform.position.z + _offsetFromTarget);
    }
}