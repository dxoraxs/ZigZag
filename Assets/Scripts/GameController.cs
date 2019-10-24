using UnityEngine;
using Zenject;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [Inject] private PlayerMovement _playerMovement;
    [Inject] private CanvasUI _canvasUI;

    public bool IsGame { get; private set; } = false;

    public void OnPlayerUpdateSpeed()
    {
        _canvasUI.OnPlayerUpdateSpeed();
    }

    public void StartGame()
    {
        IsGame = true;
    }

    public void PlayerDeath()
    {
        _canvasUI.ShowDeathPanel();
        IsGame = false;
    }

    public void PlayerInversVector()
    {
        if (!IsGame) return;
        _playerMovement.InversVectorMovement();
    }

    public void InversePauseState()
    {
        Time.timeScale = Time.timeScale> 0.5f ? 0 : 1;
        IsGame = !IsGame;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}