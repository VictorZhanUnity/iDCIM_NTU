using UnityEngine;

/// <summary>
///  資產管理模組
/// </summary>
public class DeviceManagement_Module : Module
{
    [Header(">>>CCTV互動物件管理")]
    [SerializeField] private CCTV_InteractiveManager cctvInteractiveManager;
    [Header(">>>CCTV介面管理")]
    [SerializeField] private CCTV_UIManager cctvUIManager;

    private void Awake()
    {
        cctvInteractiveManager.onToggleChanged.AddListener(OnClickModel);
    }

    private void OnClickModel(CCTV_DataHandler dataHandler)
    {
        cctvUIManager.SetDataHandler(dataHandler);
        if (dataHandler.IsSelected) onClickModel.Invoke(dataHandler.transform);
    }
}
