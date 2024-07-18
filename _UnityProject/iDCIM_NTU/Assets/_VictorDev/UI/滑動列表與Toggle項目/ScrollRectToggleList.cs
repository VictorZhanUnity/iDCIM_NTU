using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using VictorDev.UI;

/// <summary>
/// [abstract] 垂直滑動列表
/// <para> T：項目組件：ToggleItem</para>
/// <para> SO：ScriptableObject資料</para>
/// </summary>
public abstract class ScrollRectToggleList<T, SO> : MonoBehaviour where T : ToggleItem<SO> where SO : ScriptableObject
{
    [Header(">>> 列表項目組件(Prefab)")]
    [SerializeField] protected T prefabItem;

    /// <summary>
    /// 當列表項目被點擊時發送事件
    /// </summary>
    [Header(">>> 當列表項目被點擊時發送事件")]
    public UnityEvent<T> onToggleChanged;

    [Header(">>> 僅在Toggle On時才發送事件")]
    [SerializeField] private bool isInvokeWithOn = false;

    /// <summary>
    /// 僅在Toggle On時才發送事件
    /// </summary>
    public bool IsInvokeWithOn { set => isInvokeWithOn = value; }

    /// <summary>
    /// 當列表項目數量變化時
    /// </summary>
    [Header(">>> 當列表項目數量變化時")]
    public UnityEvent<int> onItemAmountChanged;

    [Header(">>> SO列表")]
    [SerializeField] private List<SO> soDataList;

    [Header(">>> 列表容器")]
    [SerializeField] private ScrollRect scrollRect;

    /// <summary>
    /// SO列表
    /// </summary>
    public List<SO> SoDataList => soDataList;

    /// <summary>
    /// 以ScriptateObject資料，建置列表資料項T
    /// </summary>
    public void SetDataList(List<SO> soDatas)
    {
        ToClearData();
        soDataList = soDatas;
        soDataList.ForEach(soData => CreateItem(soData));

        onItemAmountChanged?.Invoke(soDataList.Count);
    }

    /// <summary>
    /// 新增資料項
    /// </summary>
    protected void CreateItem(SO soData)
    {
        T item = ObjectPoolManager.GetInstanceFromQueuePool<T>(prefabItem, scrollRect.content);
        item.transform.localScale = Vector3.one;
        item.soData = soData;

        if (isInvokeWithOn)
        {
            // target為父類別實例
            item.onToggleChanged.AddListener((target) =>
            {
                if (target.isOn) onToggleChanged.Invoke(item);
            });
        }
        else
        {
            item.onToggleChanged.AddListener((target) => onToggleChanged.Invoke(item));
        }
        OnCreateEachItem(item, soData);
    }

    /// <summary>
    /// 在SetDataList裡以廻圈建立Item時，順帶觸發的函式
    /// <para>供需要額外處理的子類別使用</para>
    /// </summary>
    protected virtual void OnCreateEachItem(T item, SO soData) { }

    /// <summary>
    /// 清空資料
    /// </summary>
    public virtual void ToClearData()
    {
        //移除原列表項目至物件池
        ObjectPoolManager.PushToPool<T>(scrollRect.content);
    }

    /// <summary>
    /// 上移至頂部
    /// </summary>
    public void ScrollToTop() => scrollRect.verticalNormalizedPosition = 1f;

    /// <summary>
    /// 下移至底部
    /// </summary>
    public void ScrollToBottom() => scrollRect.verticalNormalizedPosition = 0f;

    /// <summary>
    /// 左移至最左邊
    /// </summary>
    public void ScrollToLeft() => scrollRect.horizontalNormalizedPosition = 1f;

    /// <summary>
    /// 右移至最右邊
    /// </summary>
    public void ScrollToRight() => scrollRect.horizontalNormalizedPosition = 0f;
}