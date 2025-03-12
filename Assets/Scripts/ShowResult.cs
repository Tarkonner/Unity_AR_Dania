using TMPro;
using UnityEngine;

public class ShowResult : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI boxText;

    // Update is called once per frame
    void Update()
    {
        boxText.text = Board.instance.result.ToString();
    }
}
