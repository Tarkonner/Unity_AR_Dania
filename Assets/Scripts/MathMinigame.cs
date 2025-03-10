using System.Collections;
using TMPro;
using UnityEngine;

public class MathMinigame : MonoBehaviour
{

    public static MathMinigame Instance;
    //public Board board;

    private bool isIncreasing = false;
    private bool isDecreasing = false;
    private float timer = 0f;
    private float updateRate = 1f;

    [SerializeField] TextMeshProUGUI testText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        //board.InitializeBoard();
        Board.instance.InitializeBoard();
    }

    private void Update()
    {
        
        if (isIncreasing || isDecreasing)
        {
            testText.text = Board.instance.result.ToString();
            timer += Time.deltaTime;
            if (timer >= updateRate)
            {
                if (isIncreasing) Board.instance.IncreaseResult();
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
            Debug.Log("Korrekt! Tallet stemmer.");
        }
        else
        {
            Debug.Log("Forkert! Prøv igen.");
        }
    }
}
