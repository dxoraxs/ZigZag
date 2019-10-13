using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class PlayerMovement : MonoBehaviour
{
    [Inject] private GameController _gameController;
    [SerializeField] private float _speed;
    private UnityEvent _playerFindedCrystal = new UnityEvent();
    private Vector3 _playerMovement = new Vector3(1f, 0, 1f);

    public void AddNewDelegateInUnityEvent(UnityAction function)
    {
        _playerFindedCrystal.AddListener(function);
    }

    public float GetZPositinion()
    {
        return transform.position.z;
    }

    private void Update()
    {
        if (!_gameController.IsGame) return;

        transform.Translate(_speed * _playerMovement * Time.deltaTime);
        if (Input.GetMouseButtonDown(0))
        {
            _playerMovement.x = _playerMovement.x < 0 ? 1f : -1f;
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
            _playerFindedCrystal.Invoke();
            other.GetComponent<CrystalDestroy>().OnDestroyCrystal();
        }
    }
}
