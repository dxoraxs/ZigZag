using UnityEngine;
using Zenject;

public class PlayerCrystal : MonoBehaviour
{
    [Inject] private PlayerMovement _player;
    [Inject] private CanvasUI _canvasUIText;
    private int _crystal = 0;

    private void Start()
    {
        _player.AddNewDelegateInUnityEvent(OnCoinFinded);
    }

    public void OnCoinFinded()
    {
        _crystal++;
        _canvasUIText.SetTextScore(_crystal.ToString());
    }
}