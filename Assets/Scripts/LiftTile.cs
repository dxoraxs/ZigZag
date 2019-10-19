using UnityEngine;

public class LiftTile : TileBlock
{
    [SerializeField] private float _speedMovement;
    private bool _isMoving = true;
    private float _endCoordinate;
    private StopMovingLift player;

    private void Start()
    {
        _endCoordinate = transform.position.y + 1f;
    }

    private void Update()
    {
        if (!_isMoving)
        {
            transform.Translate(transform.up * _speedMovement * Time.deltaTime);
            if (transform.position.y > _endCoordinate)
            {
                _isMoving = true;
                player.InversePause(_isMoving);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _isMoving = false;
            player = other.GetComponent<StopMovingLift>();
            player.InversePause(_isMoving);
        }
    }
}
