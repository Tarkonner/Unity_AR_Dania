using System.Collections;
using TMPro;
using UnityEngine;

public class MathMinigame : MonoBehaviour
{

    public static MathMinigame Instance;

    public Board board; 
    private Coroutine plusCoroutine;
    private Coroutine minusCoroutine;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        board.InitializeBoard();
    }

    public void StartIncreasing()
    {
        if (plusCoroutine == null)
        {
            plusCoroutine = StartCoroutine(IncreaseNumber());
        }
    }

    public void StopIncreasing()
    {
        if (plusCoroutine != null)
        {
            StopCoroutine(plusCoroutine);
            plusCoroutine = null;
        }
    }

    public void StartDecreasing()
    {
        if (minusCoroutine == null)
        {
            minusCoroutine = StartCoroutine(DecreaseNumber());
        }
    }

    public void StopDecreasing()
    {
        if (minusCoroutine != null)
        {
            StopCoroutine(minusCoroutine);
            minusCoroutine = null;
        }
    }

    private IEnumerator IncreaseNumber()
    {
        while (true)
        {
            board.IncreaseResult();
            yield return new WaitForSeconds(1);
        }
    }

    private IEnumerator DecreaseNumber()
    {
        while (true)
        {
            board.DecreaseResult();
            yield return new WaitForSeconds(1);
        }
    }

    public void CheckResult()
    {
        if (board.CheckAnswer())
        {
            Debug.Log("Korrekt! Tallet stemmer.");
        }
        else
        {
            Debug.Log("Forkert! Prøv igen.");
        }
    }
}
