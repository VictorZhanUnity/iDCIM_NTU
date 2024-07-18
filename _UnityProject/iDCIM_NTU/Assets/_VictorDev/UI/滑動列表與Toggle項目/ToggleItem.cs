using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace VictorDev.UI
{
    /// <summary>
    /// 父類別：處理為可Toggle行為之UI項目
    /// <para>+ Toggle改變時Invoke：ScriptableObject與Toggle.isOn</para>
    /// <para>+ 在子類別裡，要將子類別儲存於ScriptableObject內，以便後續接收到的類別可以對此項目進行操作</para>
    /// <para>+ 可用於ScrollView列表項目或單一UI組件</para>
    /// </summary>
    [RequireComponent(typeof(Toggle))]
    public abstract class ToggleItem<SO> : MonoBehaviour, IToggleItem where SO : ScriptableObject
    {
        [Header(">>> ScriptableObject資料")]
        [SerializeField] private SO _soData;

        [Header(">>> 點選Toggle時發送SO與Toggle.isOn")]
        public UnityEvent<IToggleItem> onToggleChanged;

        [SerializeField] protected Toggle toggle;

        public bool isOn => toggle.isOn;

        public ToggleGroup toggleGroup { set => toggle.group = value; }

        /// <summary>
        /// ScriptableObject資料
        /// </summary>
        public SO soData
        {
            get => _soData;
            set
            {
                _soData = value;
                OnSetSoData();
            }
        }

        /// <summary>
        /// 當設定完SO資料之後
        /// <para>+ 要將子類別儲存於ScriptableObject內，以便後續接收到的類別可以對此項目進行操作</para>
        /// </summary>
        protected virtual void OnSetSoData() { }

        private void Awake()
        {
            toggle.onValueChanged.AddListener((isOn) => onToggleChanged?.Invoke(this));
        }
        protected virtual void OnValidate() => toggle ??= GetComponent<Toggle>();
    }
}

public interface IToggleItem
{
    bool isOn { get; }
    ToggleGroup toggleGroup { set; }
}
