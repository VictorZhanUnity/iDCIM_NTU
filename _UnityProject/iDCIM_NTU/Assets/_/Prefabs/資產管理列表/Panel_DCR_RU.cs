using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using VictorDev.RevitUtils;

public class Panel_DCR_RU : MonoBehaviour
{
    [Header(">>> DCS����")]
    [SerializeField] private RU_DCSListItem dcsPrefab;
    [Header(">>> ��mDCS���ؤ��e��")]
    [SerializeField] private Transform ruDeviceContainer;
    [Header(">>> DCS ToggleGroup")]
    [SerializeField] private ToggleGroup toggleGroup;

    [Header(">>> �q�O")]
    [SerializeField] private ProgressBarController pbWatt;
    [Header(">>> �t��")]
    [SerializeField] private ProgressBarController pbWeight;

    [Header(">>> �I��DCS/DCN���خ�Ĳ�o")]
    public UnityEvent<RU_DCSListItem> onToggledEvent = new UnityEvent<RU_DCSListItem>();

    /// <summary>
    /// RU�C��줧����
    /// </summary>
    private float ruHeight => 25;

    private SO_DCR _soDCR { get; set; }

    /// <summary>
    /// �]�wDCR��ơA�P�ʺA�ͦ����U��DCS/DCN
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
    /// �]�wDCS�C��RU�ƦC
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

                //�I����
                item.onToggleChanged.AddListener((target) => onToggledEvent?.Invoke(target as RU_DCSListItem));
            });
            yield return null;
        }
        StartCoroutine(CreateRUDevices());
    }
}
