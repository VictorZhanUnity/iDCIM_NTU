using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace VictorDev.UIHandler
{
    /// <summary>
    /// 在Inspector裡進階處理TMP_InputField.OnValueChange事件
    /// <para>+ 直接掛在GameObject上即可</para>
    /// </summary>
    [RequireComponent(typeof(TMP_InputField))]
    public class InputFieldAdvancedEventDispatcher : MonoBehaviour
    {
        [Header(">>> Keying時是否有值")]
        public UnityEvent<bool> onKeyingEvent;
        [Header(">>> 按下Enter送出值")]
        public UnityEvent<string> onSubmitEvent;

        [SerializeField] private TMP_InputField inputFiled;

        private void Awake()
        {
            inputFiled.onValueChanged.AddListener((txtInput) =>
            {
                onKeyingEvent?.Invoke(string.IsNullOrEmpty(txtInput.Trim()) == false);
            });

            inputFiled.onSubmit.AddListener(onSubmitEvent.Invoke);

            inputFiled.onValueChanged.Invoke(inputFiled.text);
        }

        private void OnValidate() => inputFiled ??= GetComponent<TMP_InputField>();
    }
}
