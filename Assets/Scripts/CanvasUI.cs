using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class CanvasUI : MonoBehaviour
{
    [Inject] private GameController _gameController;
    [SerializeField] private GameObject _pregamePanel;
    [SerializeField] private GameObject _deathPanel;
    [SerializeField] private Text _playerScore;

    public void OnClickPregamePanel()
    {
        HidePregamePanel();
        _gameController.StartGame();
    }

    public void OnClickDeathPanel()
    {
        HideDeathPanel();
        _gameController.RestartGame();
    }

    public void SetTextScore(string score)
    {
        _playerScore.text = score;
    }

    public void ShowPregamePanel()
    {
        _pregamePanel.SetActive(true);
    }

    public void HidePregamePanel()
    {
        _pregamePanel.SetActive(false);
    }

    public void ShowDeathPanel()
    {
        _deathPanel.SetActive(true);
    }

    public void HideDeathPanel()
    {
        _deathPanel.SetActive(false);
    }
}
