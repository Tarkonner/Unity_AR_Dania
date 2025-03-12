using UnityEngine;

public class CheckButtonMarker : MonoBehaviour
{
    private void OnEnable()
    {
        MathMinigame.Instance.CheckResult();
    }

    private void OnDisable()
    {
        MathMinigame.Instance.StopDecreasing();
    }
}
