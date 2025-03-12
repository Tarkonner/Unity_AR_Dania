using System.Collections;
using TMPro;
using UnityEngine;

public class MathMinigame : MonoBehaviour
{

    [SerializeField]
    private Material correctMat, wrongMat, defaultMat;

    [SerializeField]
    private GameObject gameObj;


    public static MathMinigame Instance;
    public Board board;

    private bool isIncreasing = false;
    private bool isDecreasing = false;
    private float timer = 0f;
    private float updateRate = 1f;

    public float resetDelay = 2f;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Update()
    {
        
        if (isIncreasing || isDecreasing)
        {
            timer += Time.deltaTime;
            if (timer >= updateRate)
            {
                if (isIncreasing)
                {
                    Board.instance.IncreaseResult();
                }
                if (isDecreasing) Board.instance.DecreaseResult();
                timer = 0f; 
            }
        }
    }

    public void StartIncreasing()
    {
        isIncreasing = true;
    }

    public void StopIncreasing()
    {
        isIncreasing = false;
    }

    public void StartDecreasing()
    {
        isDecreasing = true;
        timer = 0f;
    }

    public void StopDecreasing()
    {
        isDecreasing = false;
    }

    public void CheckResult()
    {
        if (Board.instance.CheckAnswer())
        {
            gameObj.transform.GetChild(2).GetComponent<MeshRenderer>().material = correctMat;
            StartCoroutine(ResetMaterial());

            board.ResetBoard();
        }
        else
        {
            gameObj.transform.GetChild(2).GetComponent<MeshRenderer>().material = wrongMat;
            StartCoroutine(ResetMaterial());
        }
        
    }

    private IEnumerator ResetMaterial()
    {
        yield return new WaitForSeconds(resetDelay);
        gameObj.transform.GetChild(2).GetComponent<MeshRenderer>().material = defaultMat;
    }
}
