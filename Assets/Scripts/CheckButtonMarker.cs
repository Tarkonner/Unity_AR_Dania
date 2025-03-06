using UnityEngine;

public class CheckButtonMarker : MonoBehaviour
{
    private void OnEnable()
    {
        MathMinigame.Instance.CheckResult();
    }
}
