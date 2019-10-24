using Zenject;
using UnityEngine;

public class ClickInversVectorMovement : MonoBehaviour
{
    [Inject] private GameController _gameController;

    private void OnMouseDown()
    {
        _gameController.PlayerInversVector();
    }
}
