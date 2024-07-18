using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(LineRenderer))]
public abstract class LineIndicator_withToggle : LineIndicator
{
    [SerializeField] private Toggle toggle;

    [Header(">>> 當點選Toggle時")]
    public UnityEvent<Transform, bool> onToggleChanged;

    public ToggleGroup toggleGroup { set => toggle.group = value; }

    public bool IsOn
    {
        get => toggle.isOn;
        set => toggle.isOn = value;
    }

    private void Awake()
    {
        toggle.onValueChanged.AddListener((isOn) =>
        {
            onToggleChanged.Invoke(transform.parent, isOn);
        });
    }

    protected override void OnValidate()
    {
        base.OnValidate();
        toggle ??= transform.GetChild(0).GetChild(0).GetComponent<Toggle>();
    }
}
