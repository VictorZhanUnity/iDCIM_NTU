using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using VictorDev.Managers;


/// <summary>
/// ���ʪ���޲z�� - CCTV�ʵ���
/// </summary>
public class CCTV_InteractiveManager : InteractiveManager
{
    [Header(">>> CCTV_InteractiveManager")]
    [SerializeField] private ToggleGroup toggleGroup;

    [Header(">>> ���I����Indicator��")]
    public UnityEvent<SO_CCTV> OnClickIndicator;

    private List<CCTV_LineIndicator> indicatorList = new List<CCTV_LineIndicator>();

    protected override void AddMoreComponentToObject(Collider target)
    {
        CCTV_LineIndicator indicator = target.transform.GetChild(0).GetComponent<CCTV_LineIndicator>();
        indicator.toggleGroup = toggleGroup;
        indicator.onClickIndicator.AddListener(OnClickIndicator.Invoke);
        indicator.gameObject.SetActive(false);
        indicatorList.Add(indicator);

        indicator.onClickIndicator.AddListener(OnClickIndicatorHandler);
    }

    /// <summary>
    /// ���I����Indicator��
    /// </summary>
    private void OnClickIndicatorHandler(SO_CCTV soData)
    {
        Debug.Log($"SO_CCTV OnClickIndicator:");
        //Debug.Log($"SO_CCTV OnClickIndicator: {soData.URL_RTSP}");
    }

    public void SetIndicatorVisible(bool isVisible)
    {
        SetOutlineVisible(isVisible);
        indicatorList.ForEach(indicator => indicator.gameObject.SetActive(isVisible));
    }

    private void OnValidate() => toggleGroup ??= GetComponent<ToggleGroup>();
}
