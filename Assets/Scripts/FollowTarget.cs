using UnityEngine;
using Zenject;

public class FollowTarget : MonoBehaviour
{
    [SerializeField] private float _offsetFromTarget;
    [SerializeField] private float _speedMovement;
    [Inject] private PlayerMovement _targetTransform;

    private void Update()
    {
        transform.position = new Vector3(
            //_targetTransform.GetXPositinion(),
        Mathf.Lerp(transform.position.x, _targetTransform.GetXPositinion(), _speedMovement * Time.deltaTime),
        transform.position.y,
            _targetTransform.GetZPositinion() + _offsetFromTarget);
    }
}