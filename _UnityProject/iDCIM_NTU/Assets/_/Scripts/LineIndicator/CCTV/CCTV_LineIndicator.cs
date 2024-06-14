using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CCTV_LineIndicator : LineIndicator
{
    [Header(">>> CCTV_LineIndicator")]
    [SerializeField] private SO_CCTV soCCTV;
    [SerializeField] private Toggle toggle;

    [Header(">>> 當點選Toggle時，發送CCTV rtsp URL")]
    public UnityEvent<SO_CCTV> onClickIndicator;

    public ToggleGroup toggleGroup { set => toggle.group = value; }

    private void Awake()
    {
        toggle.onValueChanged.AddListener((isOn) =>
        {
            if (isOn) onClickIndicator.Invoke(soCCTV);
        });

    }

    private void OnEnable()
    {
        toggle.isOn = false; //不見得會觸發OnValueChanged事件
        toggle.onValueChanged.Invoke(false);
    }
}
