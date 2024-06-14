using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CCTV_LineIndicator : LineIndicator
{
    [Header(">>> CCTV_LineIndicator")]
    [SerializeField] private SO_CCTV soCCTV;
    [SerializeField] private Toggle toggle;

    [Header(">>> ���I��Toggle�ɡA�o�eCCTV rtsp URL")]
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
        toggle.isOn = false; //�����o�|Ĳ�oOnValueChanged�ƥ�
        toggle.onValueChanged.Invoke(false);
    }
}
