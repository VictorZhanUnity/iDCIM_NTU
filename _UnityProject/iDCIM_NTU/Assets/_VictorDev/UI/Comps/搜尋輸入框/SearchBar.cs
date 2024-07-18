using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace VictorDev.UI
{
    /// <summary>
    /// 搜尋輸入框
    /// </summary>
    [RequireComponent(typeof(TMP_InputField))]
    public class SearchBar : MonoBehaviour
    {
        [Header(">>>當點擊搜尋鈕 / OnSubmit時Invoke")]
        public UnityEvent<string> onClickSearchButton;

        [SerializeField] private TMP_InputField txtInput;
        [SerializeField] private Button btnSearch;

        /// <summary>
        /// 是否允許輸入文字與點擊搜尋按鈕
        /// </summary>
        public bool isEnabled { set => txtInput.enabled = btnSearch.enabled = value; }

        private void Awake()
        {
            txtInput.onValueChanged.AddListener((txt) => btnSearch.enabled = !string.IsNullOrEmpty(txt));
            txtInput.onValueChanged.Invoke(txtInput.text);

            txtInput.onSubmit.AddListener((txt) => OnSearchHandler());
            btnSearch.onClick.AddListener(OnSearchHandler);
        }

        private void OnSearchHandler()
        {
            if (string.IsNullOrEmpty(txtInput.text) == false)
            {
                onClickSearchButton.Invoke(txtInput.text);
            }
        }

        private void OnValidate()
        {
            txtInput ??= GetComponent<TMP_InputField>();
            btnSearch ??= transform.Find("btnSearch").GetComponent<Button>();
        }
    }
}