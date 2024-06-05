using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace VictorDev.UI.Comps
{
    /// <summary>
    /// 視窗組件 - 純線框視窗
    /// </summary>
    public class OutlinePanel : UIPanel
    {
        [SerializeField] private GameObject controllerBar;

        public bool ControllerBarVisible { set => controllerBar.SetActive(value); }

        private void OnValidate()
        {  
            controllerBar ??= transform.GetChild(0).GetChild(1).Find("ControllerBar").gameObject;
            btnClose ??= controllerBar.transform.Find("CloseButton").GetComponent<Button>();
            btnScale ??= controllerBar.transform.Find("ScaleButton").GetComponent<Button>();
            container ??= transform.Find("Outline").Find("Container");
            txtTitle ??= transform.Find("txtTitle").GetComponent<TextMeshProUGUI>();

            txtTitle.text = titleText;
        }
    }
}
