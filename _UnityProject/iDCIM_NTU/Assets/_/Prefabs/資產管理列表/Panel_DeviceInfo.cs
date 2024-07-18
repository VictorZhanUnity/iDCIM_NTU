using TMPro;
using UnityEngine;

public class Panel_DeviceInfo : MonoBehaviour
{
    public TextMeshProUGUI txt_modelNumber;
    public TextMeshProUGUI txt_elementId;
    public TextMeshProUGUI txt_deviceId;
    public TextMeshProUGUI txt_type;
    public TextMeshProUGUI txt_manufacturer;

    public SO_DCR soDCR
    {
        set
        {
            txt_modelNumber.SetText(value.modelnumber);
            txt_elementId.SetText(value.elementId);
            txt_deviceId.SetText(value.deviceId);
            txt_type.SetText(value.type);
            txt_manufacturer.SetText(value.manufacturer);
        }
    }
}
