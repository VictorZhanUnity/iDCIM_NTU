using TMPro;
using UnityEngine;
using VictorDev.UI;

/// <summary>
/// CCTV列表項目
/// </summary>
public class CCTVListItem : ToggleItem<SO_CCTV>
{
    [Header(">>> UI組件")]
    [SerializeField] private TextMeshProUGUI txtNo, txtName;

    protected override void OnSetSoData()
    {
        soData.sourceToggle = toggle;
        txtNo.text = soData.NoNumber;
        txtName.text = soData.DeviceName;
    }

    override protected void OnValidate()
    {
        base.OnValidate();
        Transform container = transform.Find("Container");
        txtNo ??= container.Find("txtNo").GetComponent<TextMeshProUGUI>();
        txtName ??= container.Find("txtName").GetComponent<TextMeshProUGUI>();

        if (soData != null) OnSetSoData();
    }
}
