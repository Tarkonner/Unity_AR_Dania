using System.Collections;
using UnityEngine;

public class CheckButtonMarker : MonoBehaviour
{
    [SerializeField]
    private Material correctMat, wrongMat, defaultMat;

    [SerializeField] private float resetDelay = 2f;
    [SerializeField] private MeshRenderer rendere;

    private void OnEnable()
    {
        if (Board.instance.CheckAnswer())
        {
            rendere.material = correctMat;
            StartCoroutine(ResetMaterial());

            Board.instance.ResetBoard();
        }
        else
        {
            rendere.material = wrongMat;
            StartCoroutine(ResetMaterial());
        }
    }

    private IEnumerator ResetMaterial()
    {
        yield return new WaitForSeconds(resetDelay);
        rendere.material = defaultMat;
    }
}

