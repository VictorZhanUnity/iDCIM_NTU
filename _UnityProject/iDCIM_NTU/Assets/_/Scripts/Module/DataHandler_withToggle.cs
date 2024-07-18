using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using VictorDev.Common;

public abstract class DataHandler_withToggle<Indicator, SO> : DataHandler<Indicator, SO> where Indicator : LineIndicator_withToggle where SO : ScriptableObject
{
    [Header(">>> 當Toggle變化時")]
    public UnityEvent<Transform, bool> onToggleChanged;

    public ToggleGroup toggleGroup { set => indicator.toggleGroup = value; }

    /// <summary>
    /// 是否為被選取
    /// </summary>
    public bool IsSelected
    {
        get => indicator.IsOn;
        set => indicator.IsOn = value;
    }
    /// <summary>
    /// 設定Toggle的EventListener
    /// </summary>
    public void InitListener()
    {
        GetComponent<ClickableObject>().OnMouseClickEvent.AddListener( (targetTrans) => IsSelected = !IsSelected);
        indicator.onToggleChanged.AddListener(OnToggleChangedHandler);
    }

    private void OnToggleChangedHandler(Transform targetTrans, bool isOn)
    {
        shaderMaterial.SetInt("_IsSelected", (IsSelected ? 1 : 0));
        onToggleChanged?.Invoke(targetTrans, isOn);
    }

    private void OnValidate() => indicator ??= transform.GetChild(0).GetComponent<Indicator>();

    private void OnDisable() => IsActivated = IsSelected = false;
}
