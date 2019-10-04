using UnityEngine;
using Zenject;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [Inject] private PlayerMovement _playerMovement;
    [Inject] private CanvasUI _canvasUI;

    public bool IsGame { get; private set; } = false;

    public void StartGame()
    {
        IsGame = true;
    }

    public void PlayerDeath()
    {
        _canvasUI.ShowDeathPanel();
        IsGame = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}
