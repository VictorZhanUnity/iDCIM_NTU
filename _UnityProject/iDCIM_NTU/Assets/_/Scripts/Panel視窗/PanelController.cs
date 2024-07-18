using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PanelController : MonoBehaviour
{
    [Header(">>>視窗標題")]
    [SerializeField] string titleText = "視窗標題";

    /// <summary>
    /// 當點擊關閉按鈕時
    /// </summary>
    [Header(">>>當點擊關閉按鈕時")]
    public UnityEvent onClickCloseButton;

    /// <summary>
    /// 當點擊放大按鈕時
    /// </summary>
    [Header(">>>當點擊放大按鈕時")]
    public UnityEvent onClickScaleButton;

    [Header(">>>UI組件")]
    [SerializeField] private TextMeshProUGUI txtTitle;
    [SerializeField] private Button btnClose, btnScale;

    /// <summary>
    /// 視窗標題
    /// </summary>
    public string Title { get => txtTitle.text; set => txtTitle.text = value; }

    private void Awake()
    {
        if (btnClose != null) btnClose.onClick.AddListener(onClickCloseButton.Invoke);
        if (btnScale != null) btnScale.onClick.AddListener(onClickScaleButton.Invoke);
    }
    private void OnValidate()
    {
        if (txtTitle != null) txtTitle.SetText(titleText);
    }
}
