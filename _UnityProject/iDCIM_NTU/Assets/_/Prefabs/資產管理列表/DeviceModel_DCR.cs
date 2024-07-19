using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using VictorDev.RevitUtils;

public class DeviceModel_DCR : DeviceModel<SO_DCR>
{

    public Dictionary<string, DeviceModel_DCSDCN> dcsdcnDict { get; private set; } = new Dictionary<string, DeviceModel_DCSDCN>();

    /// <summary>
    /// 動態建立DCS模型
    /// </summary>
    public void CreateDeviceDCSfromDict(Dictionary<string, Texture> dcsTextureDictionary, Transform prefab)
    {
        dcsdcnDict.Clear();

        soData.DCS_List.ForEach(soDCS =>
        {
            Transform device = RevitHandler.CreateDeviceDCSfromDict(soDCS, dcsTextureDictionary, prefab, this.transform);
            Vector3 scale = device.localScale;
            scale.x += 0.15f;
            device.localScale = scale;

            DeviceModel_DCSDCN modelDCSDCN = device.AddComponent<DeviceModel_DCSDCN>();
            modelDCSDCN.soData = soDCS;

            dcsdcnDict[soDCS.deviceId] = modelDCSDCN;

            modelDCSDCN.onToggleChanged.AddListener((deviceModel =>
            {
                //當DCS/DCN被點擊時觸發
                onToggleChanged?.Invoke(deviceModel);
            }));
        });
    }
}

