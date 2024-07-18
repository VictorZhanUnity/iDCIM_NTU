using UnityEngine;

/// <summary>
/// CCTV模組
/// </summary>
public class CCTV_Module : Module
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
