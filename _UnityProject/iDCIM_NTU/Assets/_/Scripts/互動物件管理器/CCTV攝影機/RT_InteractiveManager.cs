using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using VictorDev.Managers;

public class RT_InteractiveManager : InteractiveManager
{
    [Header(">>> RT_InteractiveManager")]
    [SerializeField] private ToggleGroup toggleGroup;

    [Header(">>> 當點擊到Indicator時")]
    public UnityEvent<SO_RT> OnClickIndicator;

    private List<RT_LineIndicator> indicatorList = new List<RT_LineIndicator>();

    protected override void AddMoreComponentToObject(Collider target)
    {
        RT_LineIndicator indicator = target.transform.GetChild(0).GetComponent<RT_LineIndicator>();
        indicator.toggleGroup = toggleGroup;
        indicator.onClickIndicator.AddListener(OnClickIndicator.Invoke);
        indicator.gameObject.SetActive(false);
        indicatorList.Add(indicator);

        indicator.onClickIndicator.AddListener(OnClickIndicatorHandler);
    }

    /// <summary>
    /// 當點擊到Indicator時
    /// </summary>
    private void OnClickIndicatorHandler(SO_RT soData)
    {
        Debug.Log($"SO_RT OnClickIndicator:");
        //Debug.Log($"SO_RT OnClickIndicator: {soData.Degree}");
    }


    public void SetIndicatorVisible(bool isVisible)
    {
        SetOutlineVisible(isVisible);
        indicatorList.ForEach(indicator => indicator.gameObject.SetActive(isVisible));
    }
}
