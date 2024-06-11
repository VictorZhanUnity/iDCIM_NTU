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
        [SerializeField] private float minDistance = 50f, maxDistance = 100f;
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


        /// <summary>
        /// 控制攝影機遠近
        /// </summary>
        private void CameraDistanceControll()
        {
            scrollWheelValue = Input.GetAxis("Mouse ScrollWheel") * -1;
            float maxHeight = freeLookCamera.m_Orbits[0].m_Height;
            float minHeight = freeLookCamera.m_Orbits[2].m_Height;

            for (int i = 0; i < 3; i++)
            {
#if UNITY_EDITOR || UNITY_WEBGL
                freeLookCamera.m_Orbits[i].m_Radius += scrollWheelValue * zoomSpeed;
#elif PLATFORM_ANDROID
                    freeLookCamera.m_Orbits[i].m_Radius += MobileInputHandler.ZoomByFingerScale();
#endif
                freeLookCamera.m_Orbits[i].m_Radius = Mathf.Clamp(freeLookCamera.m_Orbits[i].m_Radius, minDistance, maxDistance);
            }

            Debug.Log($"{maxHeight} / {minHeight}");

            Vector3 pos = freeLookCamera.transform.position;
            pos.y = Mathf.Clamp(pos.y + scrollWheelValue, minHeight, maxHeight);
            freeLookCamera.transform.position = pos;
        }

        private void CameraDistanceControll1()
        {
            // 获取鼠标滚轮输入
            float scrollInput = Input.GetAxis("Mouse ScrollWheel");
            if (scrollInput != 0)
            {
                // 根据输入计算新的距离
                float currentDistance = Vector3.Distance(freeLookCamera.transform.position, freeLookCamera.Follow.position);
                float targetDistance = Mathf.Clamp(currentDistance - scrollInput * zoomSpeed, minDistance, maxDistance);

                // 设置新的Follow位置
                Vector3 direction = (freeLookCamera.transform.position - freeLookCamera.Follow.position).normalized;
                freeLookCamera.Follow.position = freeLookCamera.Follow.position + direction * targetDistance;
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