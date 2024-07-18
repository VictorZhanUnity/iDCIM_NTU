using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VictorDev.Common;

public class ProgressBarController : MonoBehaviour
{
    [Header(">>> 標題文字")]
    [SerializeField] private string titleText = "標題文字";
    [Header(">>> 目前值")]
    [SerializeField] private float currentValue = 0;
    [Header(">>>最大值")]
    [SerializeField] private int maxValue = 5000;
    [Header(">>> 單位文字")]
    [SerializeField] private string unitText = "w";

    [Header(">>> 是否顯示為整數")]
    [SerializeField] private bool isWholeNumber = true;
    [Header(">>> 是否顯示最大值")]
    [SerializeField] private bool isShowMaxValue = true;

    [Header(">>> UI組件")]
    [SerializeField] private Slider slider;
    [SerializeField] private Image imgSliderBar;
    [SerializeField] private TextMeshProUGUI txtPercent, txtLabel, txtValue;

    /// <summary>
    /// 目前百分比
    /// </summary>
    public float percentage => slider.value / slider.maxValue;

    /// <summary>
    /// 目前數值
    /// </summary>
    public float value
    {
        get => slider.value;
        set
        {
            slider.value = Mathf.Max(value, 0);
            currentValue = slider.value;
            imgSliderBar.color = ColorUtils.GetColorLevelFromPercentage(percentage);

            txtValue.SetText($"{slider.value}");
            if (isShowMaxValue) txtValue.SetText($"{txtValue.text} / {slider.maxValue}");
            txtValue.SetText($"{txtValue.text} {unitText}");

            txtPercent.SetText($"{Mathf.FloorToInt(percentage * 100)}{StringHandler.SetFontSizeString(" %", 14)}");
        }
    }

    private void OnValidate()
    {
        slider ??= transform.Find("Slider").GetComponent<Slider>();
        imgSliderBar ??= slider.transform.Find("imgSliderBar").GetComponent<Image>();
        txtPercent ??= slider.transform.Find("txtPercent").GetComponent<TextMeshProUGUI>();
        txtLabel ??= transform.Find("txtLabel").GetComponent<TextMeshProUGUI>();
        txtValue ??= txtLabel.transform.Find("txtValue").GetComponent<TextMeshProUGUI>();

        slider.maxValue = maxValue;
        slider.wholeNumbers = isWholeNumber;
        txtLabel.SetText(titleText);
        value = currentValue;

        name = $"進度條 - {titleText}";
    }
}
