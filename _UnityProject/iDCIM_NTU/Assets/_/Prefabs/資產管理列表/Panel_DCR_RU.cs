using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using VictorDev.RevitUtils;

public class Panel_DCR_RU : MonoBehaviour
{
    [Header(">>> DCS項目")]
    [SerializeField] private RU_DCSListItem dcsPrefab;
    [Header(">>> 放置DCS項目之容器")]
    [SerializeField] private Transform ruDeviceContainer;
    [Header(">>> DCS ToggleGroup")]
    [SerializeField] private ToggleGroup toggleGroup;

    [Header(">>> 電力")]
    [SerializeField] private ProgressBarController pbWatt;
    [Header(">>> 負重")]
    [SerializeField] private ProgressBarController pbWeight;

    [Header(">>> 點選DCS/DCN項目時觸發")]
    public UnityEvent<RU_DCSListItem> onToggledEvent = new UnityEvent<RU_DCSListItem>();

    /// <summary>
    /// RU每單位之高度
    /// </summary>
    private float ruHeight => 25;

    private SO_DCR _soDCR { get; set; }

    /// <summary>
    /// 設定DCR資料，與動態生成底下的DCS/DCN
    /// </summary>
    private SO_DCR soDCR
    {
        get => _soDCR;
        set
        {
            _soDCR = value;
            pbWatt.value = _soDCR.watt;
            pbWeight.value = _soDCR.weight;

            SetDCSList();
        }
    }


    private DeviceModel_DCR deviceModel_DCR { get; set; }
    public void SetModelDCR(DeviceModel_DCR dcr)
    {
        deviceModel_DCR = dcr;
        soDCR = deviceModel_DCR.soData;
    }

    /// <summary>
    /// 設定DCS列表RU排列
    /// </summary>
    private void SetDCSList()
    {
        ObjectPoolManager.PushToPool<RU_DCSListItem>(ruDeviceContainer);

        IEnumerator CreateRUDevices()
        {
            soDCR.DCS_List.ForEach(soDCS =>
            {
                RU_DCSListItem item = ObjectPoolManager.GetInstanceFromQueuePool(dcsPrefab, ruDeviceContainer);
                item.toggleGroup = toggleGroup;
                item.soData = soDCS;
                item.name = $"{RevitHandler.GetGameObjectNameFormat(soDCS)} - LocationU: {soDCS.rackLocation} / HeightU: {soDCS.heightU}";

                Vector2 size = item.GetComponent<RectTransform>().sizeDelta;
                size.y = soDCS.heightU * ruHeight;
                item.GetComponent<RectTransform>().sizeDelta = size;

                float posY = (soDCS.rackLocation - 1) * ruHeight + 3 - 1056;
                item.transform.localPosition = new Vector2(0, posY);

                item.modelDCSDCN = deviceModel_DCR.dcsdcnDict[soDCS.deviceId];

                //點擊時
                item.onToggleChanged.AddListener((target) => onToggledEvent?.Invoke(target as RU_DCSListItem));
            });
            yield return null;
        }
        StartCoroutine(CreateRUDevices());
    }
}
