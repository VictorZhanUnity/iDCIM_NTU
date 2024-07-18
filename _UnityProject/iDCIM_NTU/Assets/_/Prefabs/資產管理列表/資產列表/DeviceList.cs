using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ToggleGroup))]
public class DeviceList : ScrollRectToggleList<DeviceListItem, SO_Device>
{
    [SerializeField] private ToggleGroup toggleGroup;

    private void Awake()
    {
        this.onToggleChanged.AddListener((toggleItem) =>
        {
         //   Debug.Log($"soData: {toggleItem.soData.DeviceName} / isOn:{toggleItem.isOn}");
        });
    }

    protected override void OnCreateEachItem(DeviceListItem item, SO_Device soData)
    {
        item.toggleGroup = toggleGroup;
    }

    private void OnValidate()
    {
        toggleGroup ??= GetComponent<ToggleGroup>();
    }
}
