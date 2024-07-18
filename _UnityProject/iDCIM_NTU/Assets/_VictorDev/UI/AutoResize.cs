using UnityEngine;

namespace VictorDev.UI
{
    /// <summary>
    /// 自動調整尺吋大小
    /// <para>+ 直接掛載到GameObject下即可</para>
    /// </summary>
    public class AutoResize : MonoBehaviour
    {
        [Header(">>> 解析度")]
        [SerializeField] private Vector2 resolution = new Vector2(1920, 1080);
        [Header(">>> 是否強迫為指定解析度")]
        [SerializeField] private bool isForcedResolution = false;

        [Space(10)]
        [SerializeField] private RectTransform rectTransform;

        private void Start()
        {

        }

        private void OnValidate()
        {
            rectTransform ??= GetComponent<RectTransform>();
            Resize();
        }

        private void Resize()
        {
            Canvas.ForceUpdateCanvases(); //強迫Canvas更新一幀

            //指定尺吋
            if (isForcedResolution) rectTransform.sizeDelta = resolution;
            else
            {
                // 設置新的尺寸，根據新的寬度等比例調整高度
                float newHeight = resolution.y * (rectTransform.rect.width / resolution.x);
                // 設置新的尺寸

                rectTransform.sizeDelta = new Vector2(rectTransform.rect.width, newHeight);
            }
        }
    }
}
