using UnityEngine;

public class MinusMarker : MonoBehaviour
{
    private void OnEnable()
    {
        MathMinigame.Instance.StartDecreasing();
    }

    private void OnDisable()
    {
        MathMinigame.Instance.StopDecreasing();
    }
}
