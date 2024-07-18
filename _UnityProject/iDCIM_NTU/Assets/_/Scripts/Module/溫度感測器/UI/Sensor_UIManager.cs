using UnityEngine;

public class Sensor_UIManager : UIManager
{
    private Sensor_DataHandler dataHandler;

    public void SetDataHandler(Sensor_DataHandler dataHandler)
    {
        Debug.Log($"Sensor_UIManager: {dataHandler.name} / isOn: {dataHandler.IsSelected}");
        this.dataHandler = dataHandler;
    }

    [ContextMenu("Send Event")]
    private void onClickClosePanelHandler()
    {
        dataHandler.IsSelected = false;
    }
}
