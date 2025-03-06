using UnityEngine;

public class PlusMarker : MonoBehaviour
{
    private void OnEnable()
    {
        MathMinigame.Instance.StartIncreasing();
    }

    private void OnDisable()
    {
        MathMinigame.Instance.StopIncreasing();
    }
}
