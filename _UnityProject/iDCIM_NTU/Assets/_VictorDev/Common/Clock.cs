using System;
using System.Collections;
using System.Globalization;
using TMPro;
using UnityEngine;

namespace VictorDev.Common
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class Clock : MonoBehaviour
    {
        [Header(">>> 是否為24小時制")]
        [SerializeField] private bool is24Hrs = true;

        [Header(">>> 是否顯示秒數")]
        [SerializeField] private bool isShowSec = false;

        [Header(">>> AM/PM語言")]
        [SerializeField] private enumLang lang = enumLang.英文;

        [Space(10)]
        [SerializeField] private TextMeshProUGUI txtTime;

        private CultureInfo cultureInfo { get; set; }
        private string langFormat
        {
            get
            {
                switch (lang)
                {
                    case enumLang.中文: return "zh-CN";
                    case enumLang.英文: return "en-US";
                }
                return "";
            }
        }

        private void Start()
        {
            IEnumerator enumerator()
            {
                while (true)
                {
                    UpdateClock();
                    yield return new WaitForSeconds(1f); // 每秒更新一次
                }
            }
            cultureInfo = new CultureInfo(langFormat);
            StartCoroutine(enumerator());
        }
        private void UpdateClock()
        {
            string format = (is24Hrs) ? "HH:mm" : "tt hh:mm";
            if (isShowSec) format += ":ss";

            txtTime.SetText(DateTime.Now.ToString(format, cultureInfo));
        }

        private void OnValidate()
        {
            txtTime ??= GetComponent<TextMeshProUGUI>();
            if (txtTime != null)
            {
                cultureInfo = new CultureInfo(langFormat);
                UpdateClock();
            }
        }
        private enum enumLang { 中文, 英文 }
    }
}
