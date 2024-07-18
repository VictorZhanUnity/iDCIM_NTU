using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VictorDev.Common;
using VictorDev.UI;

/// <summary>
/// �q�O�t��UI����
/// </summary>
[RequireComponent(typeof(Slider))]
public class VoltageUIController : MonoBehaviour
{
    [SerializeField] private float curentValue = 0;
    [SerializeField] private float maxValue = 5000;

    [Header(">>> UI�ե�")]
    [SerializeField] private Slider slider;
    [SerializeField] private Image imgFilled;
    [SerializeField] private TextMeshProUGUI txtPercent;
    [SerializeField] private TextMeshProUGUI txtValue;

    public int MaxValue
    {
        set
        {
            maxValue = value;
            RefreshData();
        }
    }
    public int Value
    {
        set
        {
            curentValue = Mathf.Max(value, 0);
            RefreshData();
        }
    }

    private void OnValidate()
    {
        slider ??= GetComponent<Slider>();
        RefreshData();
    }

    private void RefreshData()
    {
        if (txtPercent != null)
        {
            curentValue = Mathf.Max(curentValue, 0);

            float percent = curentValue / maxValue;
            txtPercent.text = $"{Mathf.FloorToInt(percent * 100)}{HtmlTagHandler.ToSetSize("%", 14)}";
            slider.value = percent;

            //��s�C��
            Color color = Color.green;
            if (percent > 0.8f) color = ColorUtils.colorRed;
            else if (percent > 0.6f) color = ColorUtils.colorOrange;
            else if (percent > 0.4f) color = Color.yellow;
            imgFilled.color = color;

            txtPercent.color = color;
        }

        if (txtValue != null)
        {
            txtValue.text = $"{curentValue} / {maxValue} W";
        }
    }
}
