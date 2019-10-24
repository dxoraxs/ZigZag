using UnityEngine;
using Zenject;

public class PlayerCrystal : MonoBehaviour
{
    [Inject] private CanvasUI _canvasUIText;
    private int _crystal = 0;

    public void OnCoinFinded()
    {
        _crystal++;
        _canvasUIText.SetTextScore(_crystal);
    }

    public void SetTextDistance(int distance)
    {
        _canvasUIText.SetTextDistance(distance);
    }
}