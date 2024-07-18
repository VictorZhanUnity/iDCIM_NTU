using TMPro;
using UnityEngine;

public class RU_Controller : MonoBehaviour
{
    public string num;

    public TextMeshProUGUI num1, num2;

    private void OnValidate()
    {
        num = (42- transform.GetSiblingIndex()).ToString();
        num1.text = num2.text = num;
        gameObject.name = "RU佈局條";
    }
}
