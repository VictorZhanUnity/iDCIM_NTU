using System;
using TMPro;
using UnityEngine;
using VictorDev.RevitUtils;
using VictorDev.UI;

public class RU_DCSListItem : ToggleItem<SO_DCSDCN>, IDeviceModel
{
    public DeviceModel_DCSDCN modelDCSDCN { get; set; }

    [SerializeField] private TextMeshProUGUI txtDeviceName;

    public bool isSelected { get => isOn; set { } }

    public string system => soData.system;

    private void Start()
    {
        onToggleChanged.AddListener(onToggleChangedHandler);
    }

    private void onToggleChangedHandler(IToggleItem target)
    {
        modelDCSDCN.isSelected = target.isOn;
    }

    protected override void OnSetSoData()
    {
        string deviceType = RevitHandler.GetDCSTypeFromDeviceID(soData.deviceId);
        txtDeviceName.SetText(deviceType);
    }
}
