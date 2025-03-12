using System.Collections;
using TMPro;
using UnityEngine;

public class MathMinigame : MonoBehaviour
{

    [SerializeField]
    private GameObject gameObj;


    public static MathMinigame Instance;

    private bool isIncreasing = false;
    private bool isDecreasing = false;
    private float timer = 0f;
    private float updateRate = 1f;




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


}
