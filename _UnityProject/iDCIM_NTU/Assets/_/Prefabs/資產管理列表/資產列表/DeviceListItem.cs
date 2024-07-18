using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using VictorDev.UI;

public class DeviceListItem : ToggleItem<SO_Device>
{
    [Header(">>> UI組件")]
    [SerializeField] private TextMeshProUGUI txtDeviceName;
    [SerializeField] private TextMeshProUGUI txtDeviceCode;
    [SerializeField] private Toggle isNeedRepair;

    protected override void OnSetSoData()
    {
      /*  soData.deviceListItem = this;

        txtDeviceName.SetText(soData.DeviceName);
        txtDeviceCode.SetText(soData.DeviceCode);*/
    }

   
}
