using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RT_LineIndicator : LineIndicator
{
    [Header(">>> RT_LineIndicator")]
    [SerializeField] private SO_RT soRT;
    [SerializeField] private Toggle toggle;
    [SerializeField] private TextMeshProUGUI txtDegree;

    [Header(">>> 當點選Toggle時")]
    public UnityEvent<SO_RT> onClickIndicator;

    public ToggleGroup toggleGroup { set => toggle.group = value; }

    /// <summary>
    /// 設定溫度度數
    /// </summary>
    public float Degree { set => txtDegree.SetText(value.ToString("F0")); }

    private void Awake()
    {
        toggle.onValueChanged.AddListener((isOn) =>
        {
            if (isOn) onClickIndicator.Invoke(soRT);
        });
    }

    private void OnEnable()
    {
        toggle.isOn = false; //不見得會觸發OnValueChanged事件
        toggle.onValueChanged.Invoke(false);
    }
}
