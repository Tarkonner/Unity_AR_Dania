using TMPro;
using UnityEngine;

public class Board : MonoBehaviour
{
    public TextMeshProUGUI platform1Text;
    public TextMeshProUGUI platform3Text;
    public TextMeshProUGUI platform5Text;

    private int number1;
    private int number3;
    private int result;

    public void InitializeBoard()
    {
        number1 = Random.Range(1, 11);
        number3 = Random.Range(1, 11);
        result = 0;

        UpdateUI();
    }

    void Update()
    {
        platform5Text.text = result.ToString();
    }

    private void UpdateUI()
    {
        Invoke(nameof(ForceUpdateUI), 0f);
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
        platform5Text.text = result.ToString();
        UpdateUI();
    }

    public void DecreaseResult()
    {
        result--;
        platform5Text.text = result.ToString();
        UpdateUI();
    }

    public bool CheckAnswer()
    {
        return result == (number1 + number3);
    }
}
