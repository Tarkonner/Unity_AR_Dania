using UnityEngine;
using System.Collections;
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
