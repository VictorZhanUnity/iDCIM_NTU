using Cinemachine;
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
        [SerializeField] private float minRange = 50f, maxRange = 100f;
        [SerializeField] private float minPosY = 0f, maxPosY = 100f;
        [SerializeField] private float zoomSpeed = 30f;
        [SerializeField] private float moveSpeed = 30f;

        [Space(20)]
        [SerializeField] private CinemachineFreeLook freeLookCamera;
        [SerializeField] private Transform lookAtTarget;

        private float scrollWheelValue;
        private Vector3 movePosVector = new Vector3();

        private void Update()
        {
            CameraDistanceControll();
            CameraPosYControll();
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

        /// <summary>
        /// 控制攝影機遠近
        /// </summary>
        private void CameraDistanceControll()
        {
            scrollWheelValue = Input.GetAxis("Mouse ScrollWheel") * -1;
            for (int i = 0; i < 3; i++)
            {
#if UNITY_EDITOR || UNITY_WEBGL
                freeLookCamera.m_Orbits[i].m_Radius += scrollWheelValue * zoomSpeed;
#elif PLATFORM_ANDROID
                    freeLookCamera.m_Orbits[i].m_Radius += MobileInputHandler.ZoomByFingerScale();
#endif
                freeLookCamera.m_Orbits[i].m_Radius = Mathf.Clamp(freeLookCamera.m_Orbits[i].m_Radius, minRange, maxRange);
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