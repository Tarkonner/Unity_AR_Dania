using TMPro;
using UnityEngine;

public class Board : MonoBehaviour
{
    public static Board instance;

    public TextMeshProUGUI platform1Text;
    public TextMeshProUGUI platform3Text;
    public TextMeshProUGUI platform5Text;

    private int number1;
    private int number3;
    public int result { get; private set; }

    private void Awake()
    {
        instance = this;

        number1 = Random.Range(1, 11);
        number3 = Random.Range(1, 11);
        result = 0;

        ForceUpdateUI();
    }

    private void ForceUpdateUI()
    {
        platform1Text.text = number1.ToString();
        platform3Text.text = number3.ToString();
        platform5Text.text = result.ToString();
    }

    public void IncreaseResult()
    {
        result++;
        ForceUpdateUI();
    }

    public void DecreaseResult()
    {
        result--;
        ForceUpdateUI();
    }

    public bool CheckAnswer()
    {
        return result == (number1 + number3);
    }
}
