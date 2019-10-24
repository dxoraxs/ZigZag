using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class PlayerMovement : MonoBehaviour
{
    [Inject] private GameController _gameController;
    [Inject] private PlayerCrystal _playerCrystal;
    [SerializeField] private float _speed;
    //private UnityEvent _playerFindedCrystal = new UnityEvent();
    private Vector3 _playerMovement = new Vector3(1f, 0, 1f);
    private int _lastDistance = 0;

    //public void AddNewDelegateFindedCrystal(UnityAction function)
    //{
    //    _playerFindedCrystal.AddListener(function);
    //}

    public void InversVectorMovement()
    {
        _playerMovement.x = _playerMovement.x < 0 ? 1f : -1f;
    }

    public float GetZPositinion()
    {
        return transform.position.z;
    }

    public float GetXPositinion()
    {
        return transform.position.x;// - _playerMovement.x;
    }

    private void OnPlayerUpdateSpeed()
    {
        _speed += 0.1f;
        _gameController.OnPlayerUpdateSpeed();
    }

    private void Update()
    {
        if (!_gameController.IsGame) return;

        transform.Translate(_speed * _playerMovement * Time.deltaTime);

        if ((int)transform.position.z > _lastDistance)
        {
            _lastDistance = (int)transform.position.z;
            _playerCrystal.SetTextDistance(_lastDistance);
            if (_lastDistance % 100 ==0)
            {
                OnPlayerUpdateSpeed();
            }
        }

        if (transform.position.y < -4)
        {
            _gameController.PlayerDeath();
            transform.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Crystal")
        {
            //_playerFindedCrystal.Invoke();
            _playerCrystal.OnCoinFinded();
            other.GetComponent<CrystalDestroy>().OnDestroyCrystal();
        }
    }
}
