using UnityEngine;
using System.Collections;
public class PlusMarker : MonoBehaviour
{
    //private void Start()
    //{
    //    StartCoroutine(Increase());
    //}

    //IEnumerator Increase()
    //{
    //    yield return new WaitForSeconds(1);
    //    StartCoroutine(Increase());

    //}

    private void OnEnable()
    {
        MathMinigame.Instance.StartIncreasing();
    }

    private void OnDisable()
    {
        MathMinigame.Instance.StopIncreasing();
    }


    public void Test()
    {
        MathMinigame.Instance.StartIncreasing();
    }
}
