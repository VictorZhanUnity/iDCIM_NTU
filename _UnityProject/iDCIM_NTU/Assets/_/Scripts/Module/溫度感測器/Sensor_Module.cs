using UnityEngine;

public class Sensor_Module : Module
{
    [Header(">>>Sensor互動物件管理")]
    [SerializeField] private Sensor_InteractiveManager SensorInteractiveManager;
    [Header(">>>Sensor介面管理")]
    [SerializeField] private Sensor_UIManager sensorUIManager;

    private void Awake()
    {
        SensorInteractiveManager.onToggleChanged.AddListener(OnClickModel);
    }

    private void OnClickModel(Sensor_DataHandler dataHandler)
    {
        sensorUIManager.SetDataHandler(dataHandler);
        if (dataHandler.IsSelected) onClickModel.Invoke(dataHandler.transform);
    }
}
