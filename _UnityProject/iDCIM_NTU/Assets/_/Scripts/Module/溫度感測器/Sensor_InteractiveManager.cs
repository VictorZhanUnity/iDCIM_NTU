using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using VictorDev.Managers;

/// <summary>
/// 互動物件管理器 - 感測器
/// </summary>
public class Sensor_InteractiveManager : InteractiveManager
{
    [SerializeField] private ToggleGroup toggleGroup;

    [Header(">>> 當Toggle狀態改變時")]
    public UnityEvent<Sensor_DataHandler> onToggleChanged;

    private Dictionary<Transform, Sensor_DataHandler> dataHandlerDict { get; set; } = new Dictionary<Transform, Sensor_DataHandler>();

    /// <summary>
    /// Sensor_DataHandler
    /// </summary>
    private Sensor_DataHandler selectedDataHandler = null;

    protected override void AddMoreComponentToObject(Collider target)
    {
        Sensor_DataHandler dataHandler = target.GetComponent<Sensor_DataHandler>();
        dataHandler.InitListener();
        dataHandler.toggleGroup = toggleGroup;
        dataHandler.onToggleChanged.AddListener(OnToggleChangedHandler);

        dataHandlerDict[target.transform] = dataHandler;
    }

    /// <summary>
    /// 當點選圖標Toggle或直接改變Toggle狀態時
    /// </summary>
    private void OnToggleChangedHandler(Transform targetTrans, bool isOn)
    {
        //取消目標選取狀態
        if (isOn == false) dataHandlerDict[targetTrans].IsSelected = false;
        else
        {
            if (selectedDataHandler != null)
            {
                //判斷點擊的對像是否為同一個
                if (dataHandlerDict[targetTrans] != selectedDataHandler)
                {
                    selectedDataHandler.IsSelected = false;
                    selectedDataHandler = dataHandlerDict[targetTrans];
                }
            }
            dataHandlerDict[targetTrans].IsSelected = true;
        }
        onToggleChanged.Invoke(dataHandlerDict[targetTrans]);
    }

    /// <summary>
    /// 設置Indicator顯示/隱藏 (供主選單Toggle使用)
    /// </summary>
    public void SetIndicatorVisible(bool isVisible)
    {
        SetOutlineVisible(isVisible);
        foreach (Transform target in dataHandlerDict.Keys)
        {
            dataHandlerDict[target].IsActivated = isVisible;
            if(isVisible == false) dataHandlerDict[target].IsSelected = false;
        }
    }
    private void OnValidate() => toggleGroup ??= GetComponent<ToggleGroup>();
}
