using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using VictorDev.Common;

namespace VictorDev.Managers
{
    /// <summary>
    /// 互動物件管理器
    /// <para>將目標對像加上鼠標移入、點擊、移出之互動事件</para>
    /// <para>將目標對像加上Outline外框特效</para>
    /// </summary>
    public class InteractiveManager : MonoBehaviour
    {
        [Header(">>> 欲設定互動功能的物件對像")]
        [SerializeField]
        protected List<Collider> interactiveObjects;

        [Header(">>> 在正常情況時是否顯示Outline")]
        [SerializeField] private bool isOutlineVisibleInNormal = false;

        [Header(">>> 正常情況時(鼠標移出時)")]
        [SerializeField] private float outLineWidth_Normal = 1;
        [SerializeField] private Color color_Normal = Color.white;

        [Header(">>> 當鼠標移入時")]
        [SerializeField] private float outLineWidth_OnMouseOver = 2;
        [SerializeField] private Color color_OnMouseOver = Color.yellow;

        private List<Outline> outlines { get; set; } = new List<Outline>();

        [Header(">>> 點擊互動物件時")]
        public UnityEvent<Transform> OnMouseClickInteractiveItemEvent = new UnityEvent<Transform>();

        private void Start()
        {
            for (int i = 0; i < interactiveObjects.Count; i++)
            {
                SetupInteractive(interactiveObjects[i]);
            }
        }

        /// <summary>
        /// 設定互動物件功能
        /// </summary>
        private void SetupInteractive(Collider target)
        {
            Outline outline = target.AddComponent<Outline>();
            outline.OutlineMode = Outline.Mode.OutlineVisible;
            OnMouseExitEvent(outline);

            outlines.Add(outline);

            ClickableObject clickableObject = target.AddComponent<ClickableObject>();
            clickableObject.OnMouseEnterEvent += (target) => OnMouseEnterEvent(outline);
            clickableObject.OnMouseExitEvent += (target) => OnMouseExitEvent(outline);
            clickableObject.OnMouseClickEvent += (target) => OnMouseClick(outline);

            AddMoreComponentToObject(target);
        }

        protected virtual void AddMoreComponentToObject(Collider target)
        {
        }

        public void SetOutlineVisible(bool visible)
        {
            isOutlineVisibleInNormal = visible;
            outlines.ForEach(target => target.OutlineWidth = (isOutlineVisibleInNormal) ? outLineWidth_Normal : 0);
        }

        private void OnMouseEnterEvent(Outline outline)
        {
            outline.OutlineWidth = outLineWidth_OnMouseOver;
            outline.OutlineColor = color_OnMouseOver;
        }

        private void OnMouseExitEvent(Outline outline)
        {
            outline.OutlineWidth = (isOutlineVisibleInNormal) ? outLineWidth_Normal : 0;
            outline.OutlineColor = color_Normal;
        }

        private void OnMouseClick(Outline outline) => OnMouseClickInteractiveItemEvent?.Invoke(outline.transform);
    }
}
