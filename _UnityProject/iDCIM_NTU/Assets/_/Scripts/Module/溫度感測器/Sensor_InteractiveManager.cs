using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using VictorDev.Managers;

/// <summary>
/// ���ʪ���޲z�� - �P����
/// </summary>
public class Sensor_InteractiveManager : InteractiveManager
{
    [SerializeField] private ToggleGroup toggleGroup;

    [Header(">>> ��Toggle���A���ܮ�")]
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
    /// ���I��ϼ�Toggle�Ϊ�������Toggle���A��
    /// </summary>
    private void OnToggleChangedHandler(Transform targetTrans, bool isOn)
    {
        //�����ؼп�����A
        if (isOn == false) dataHandlerDict[targetTrans].IsSelected = false;
        else
        {
            if (selectedDataHandler != null)
            {
                //�P�_�I�����ﹳ�O�_���P�@��
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
    /// �]�mIndicator���/���� (�ѥD���Toggle�ϥ�)
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
