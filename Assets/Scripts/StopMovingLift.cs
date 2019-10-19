using UnityEngine;

public class StopMovingLift : MonoBehaviour
{
    private PlayerMovement _playerMovement;

    private void Start()
    {
        _playerMovement = GetComponent<PlayerMovement>();
    }

    public void InversePause(bool flag)
    {
        _playerMovement.enabled = flag;
    }
}
