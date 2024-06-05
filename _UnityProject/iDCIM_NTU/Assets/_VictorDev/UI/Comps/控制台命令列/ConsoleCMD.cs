using TMPro;
using UnityEngine;
using VictorDev.Common;

namespace VictorDev.UI.Comps
{
    /// <summary>
    /// 控制台命令列
    /// </summary>
    public class ConsoleCMD : SingletonMonoBehaviour<ConsoleCMD>
    {
        [Header(">>> Console控制台文字字串")]
        [TextArea(1, 5)]
        [SerializeField] private string consoleText;
        [Header(">>> 輸入框文字字串")]
        [SerializeField] private string cmdText;

        [Space(10)]
        [SerializeField] private TextMeshProUGUI txtConsole;
        [SerializeField] private TMP_InputField inputLine;

        private void Awake()
        {
            inputLine.onSubmit.AddListener((inputStr) =>
            {
                inputLine.text = "";
                ReceiveCMD(inputStr);
            });
        }

        public static void ReceiveCMD(string cmdString)
        {
            if (cmdString.ToLower() == "cls") Instance.txtConsole.SetText("");
            else PrintOut(cmdString);
        }

        public static void PrintOut(string msg) => Instance.txtConsole.text += $"\n{msg}";

        protected override void OnValidateAfter()
        {
            name += " - 控制台命令列";
            inputLine ??= transform.GetChild(1).GetComponent<TMP_InputField>();

            txtConsole?.SetText(consoleText);
            inputLine.text = cmdText;
        }
    }

}
