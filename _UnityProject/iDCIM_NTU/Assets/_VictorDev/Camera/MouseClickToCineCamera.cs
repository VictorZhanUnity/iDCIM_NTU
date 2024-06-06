using Cinemachine;
using UnityEngine;

namespace VictorDev.CameraHandler
{
    /// <summary>
    /// 直接掛載在Cinamachine Camera組件上就好
    /// <para>當滑鼠左鍵按下時，才能進行Cinemachine攝影機控制</para>
    /// </summary>
    public class MouseClickToCineCamera : MonoBehaviour
    {
        [Header(">>> 滑鼠移動速度")]
        [SerializeField] private int speed_X = 150;
        [SerializeField] private int speed_Y = 2;

        private enum enumMouseType { LeftClick, RightClick };
        [Header(">>> 滑鼠按鍵行為")]
        [SerializeField] private enumMouseType clickBehaviour = enumMouseType.LeftClick;

        private CinemachineFreeLook freeLookCamera;
        private int keyValue;

        private void Start() => freeLookCamera = GetComponent<CinemachineFreeLook>();

        void Update()
        {
            switch (clickBehaviour)
            {
                case enumMouseType.LeftClick:
                    keyValue = 0;
                    break;
                case enumMouseType.RightClick:
                    keyValue = 1;
                    break;
            }

            //偵測是否支援觸控(Web有支援)
            bool isTouched = (Input.touchSupported) ? (Input.touchCount == 1) : false;

            // Rotate camera if allowed
            if (isTouched || Input.GetMouseButton(keyValue))
            {
                freeLookCamera.m_YAxis.m_MaxSpeed = speed_Y;
                freeLookCamera.m_XAxis.m_MaxSpeed = speed_X;
            }
            else
            {
                freeLookCamera.m_YAxis.m_MaxSpeed = 0;
                freeLookCamera.m_XAxis.m_MaxSpeed = 0;
            }
        }
    }
}
