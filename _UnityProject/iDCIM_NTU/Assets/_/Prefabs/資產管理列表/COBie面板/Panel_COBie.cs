using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Panel_COBie : MonoBehaviour
{
    [Header(">>> COBie列表項目Prefab")]
    [SerializeField] private COBieListItem listItemPrefab;
    [Header(">>> COBie列表")]
    [SerializeField] private ScrollRect scrollRect;
    [Header(">>> 當面板關閉時Invoke elementId")]
    public UnityEvent<string> onClosePanel = new UnityEvent<string>();

    [SerializeField] private PanelController panelController;
    [SerializeField] private MoveFadeController moveFadeController;

    private void Awake() => panelController.onClickCloseButton.AddListener(() =>
    {
        onClosePanel.Invoke(_soData.elementId);
        gameObject.SetActive(false);
    });


    private SO_COBie _soData { get; set; }
    public SO_COBie soData
    {
        set
        {
            _soData = value;
            ObjectPoolManager.PushToPool<COBieListItem>(scrollRect.content);

            Dictionary<string, string> dataSet = _soData.prop_NoNull;
            foreach (string key in dataSet.Keys)
            {
                COBieListItem item = ObjectPoolManager.GetInstanceFromQueuePool(listItemPrefab, scrollRect.content);
                item.key = key;
                item.value = dataSet[key];
            }
            panelController.Title = _soData.deviceName;
            gameObject.SetActive(true);
            moveFadeController.Play();
        }
    }

    private void OnValidate()
    {
        panelController ??= GetComponent<PanelController>();
        moveFadeController ??= GetComponent<MoveFadeController>();
    }
}
