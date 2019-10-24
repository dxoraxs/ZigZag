using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class CanvasUI : MonoBehaviour
{
    [Inject] private GameController _gameController;
    [SerializeField] private Animation _animationPlayerUpdateSpeed;
    [SerializeField] private GameObject _pregamePanel;
    [SerializeField] private GameObject _gamePanel;
    [SerializeField] private GameObject _deathPanel;
    [SerializeField] private Text _playerScore;
    [SerializeField] private Text _playerDistance;
    [SerializeField] private Text _deathPanelTextScore;
    [SerializeField] private Text _deathPanelTextDistance;
    private int _score, _distance;

    public void OnPlayerUpdateSpeed()
    {
        _animationPlayerUpdateSpeed.Play();
        Debug.Log("Update speed");
    }

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

    public void OnClickButtonPause()
    {
        _gameController.InversePauseState();
    }

    public void SetTextScore(int score)
    {
        _playerScore.text = score.ToString();
        _score = score;
    }

    public void SetTextDistance(int distance)
    {
        _playerDistance.text = distance.ToString();
        _distance = distance;
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
        HideGamePanel();
        _deathPanelTextScore.text += _score;
        _deathPanelTextDistance.text += _distance;
    }

    public void HideDeathPanel()
    {
        _deathPanel.SetActive(false);
    }

    public void HideGamePanel()
    {
        _gamePanel.SetActive(false);
    }

    private void Start()
    {
        _pregamePanel.SetActive(true);
        _gamePanel.SetActive(false);
        _deathPanel.SetActive(false);
}
}