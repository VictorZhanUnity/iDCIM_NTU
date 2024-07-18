using Cinemachine;
using System.Collections;
using UnityEngine;

namespace VictorDev.CameraHandler
{
    /// <summary>
    ///  直接掛載在GameObject上就好
    ///  <para>+ 再設置CinemachineFreeLook的Target</para>
    /// </summary>
    [RequireComponent(typeof(CinemachineFreeLook))]
    public class ZoomWithMouseWheel : MonoBehaviour
    {
        [SerializeField] private float minDistance = 50f, maxDistance = 100f;
        [SerializeField] private float minPosY = 0f, maxPosY = 100f;
        [SerializeField] private float zoomSpeed = 30f;
        [SerializeField] private float moveSpeed = 30f;

        [Space(20)]
        [SerializeField] private CinemachineFreeLook freeLookCamera;
        [SerializeField] private Transform lookAtTarget;

        private float scrollWheelValue;
        private Vector3 movePosVector = new Vector3();

        /// <summary>
        /// 改變範圍半徑
        /// </summary>
        public float OrbitRadius
        {
            set
            {
                StopCoroutine(LerpDistance());
                StartCoroutine(LerpDistance());

                for (int i = 0; i < 3; i++)
                {
                    freeLookCamera.m_Orbits[i].m_Radius = Mathf.Clamp(value, minDistance, maxDistance);
                }
            }
        }



        IEnumerator LerpDistance()
        {
            float duration = 3;
            float elapsedTime = 0;
            while (Mathf.Round(freeLookCamera.m_YAxis.Value * 100) / 100 > 0f)
            {
                yield return new WaitForEndOfFrame();
                elapsedTime += Time.deltaTime;

                // 使用 Mathf.Lerp 实现平滑的过渡
                freeLookCamera.m_YAxis.Value = Mathf.Lerp(freeLookCamera.m_YAxis.Value, 0, elapsedTime / duration);
            }

        }

        private void Update()
        {
            CameraDistanceControll();
            CameraPosYControll();
        }


        /// <summary>
        /// 控制攝影機遠近
        /// </summary>
        private void CameraDistanceControll()
        {
            scrollWheelValue = Input.GetAxis("Mouse ScrollWheel") * -1;
            float maxHeight = freeLookCamera.m_Orbits[0].m_Height;
            float minHeight = freeLookCamera.m_Orbits[2].m_Height;

#if UNITY_EDITOR || UNITY_WEBGL
            AdjustRadius(scrollWheelValue * zoomSpeed);
#elif PLATFORM_ANDROID
          //  AdjustRadius(MobileInputHandler.ZoomByFingerScale());
#endif

            Vector3 pos = freeLookCamera.transform.position;
            pos.y = Mathf.Clamp(pos.y + scrollWheelValue, minHeight, maxHeight);
            freeLookCamera.transform.position = pos;
        }

        /// <summary>
        /// 調整範圍半徑
        /// </summary>
        private void AdjustRadius(float value)
        {
            for (int i = 0; i < 3; i++)
            {
                freeLookCamera.m_Orbits[i].m_Radius += value;
                freeLookCamera.m_Orbits[i].m_Radius = Mathf.Clamp(freeLookCamera.m_Orbits[i].m_Radius, minDistance, maxDistance);
            }
        }

        private void CameraPosYControll()
        {
            // Move up with Q key
            if (Input.GetKey(KeyCode.Q))
            {
                // Ensure the new Y position doesn't go above the maximum
                movePosVector.x = lookAtTarget.position.x;
                movePosVector.y = Mathf.Clamp(lookAtTarget.position.y + moveSpeed * Time.deltaTime, minPosY, maxPosY);
                movePosVector.z = lookAtTarget.position.z;
                lookAtTarget.position = movePosVector;
            }

            // Move down with E key
            if (Input.GetKey(KeyCode.E))
            {
                // Ensure the new Y position doesn't go below the minimum
                movePosVector.x = lookAtTarget.position.x;
                movePosVector.y = Mathf.Clamp(lookAtTarget.position.y - moveSpeed * Time.deltaTime, minPosY, maxPosY);
                movePosVector.z = lookAtTarget.position.z;
                lookAtTarget.position = movePosVector;
            }
        }

        private void OnValidate()
        {
            freeLookCamera = GetComponent<CinemachineFreeLook>();
            freeLookCamera.Follow = lookAtTarget;
            freeLookCamera.LookAt = lookAtTarget;
        }
    }
}