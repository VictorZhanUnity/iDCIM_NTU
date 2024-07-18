using UnityEngine;
using Cinemachine;

public class MiddleMouseDrag : MonoBehaviour
{
    public float moveSpeed = 0.1f;  // 移动速度
    public CinemachineFreeLook cinemachineFreeLook; // 引用到你的Cinemachine Free Look组件

    private Vector3 lastMousePosition;

    void Update()
    {
        // 检测鼠标中键是否按下
        if (Input.GetMouseButton(1))
        {
            // 获取当前鼠标位置
            Vector3 currentMousePosition = Input.mousePosition;

            // 计算鼠标移动量
            Vector3 deltaMousePosition = currentMousePosition - lastMousePosition;

            // 获取主相机的前方向和右方向
            Transform cameraTransform = Camera.main.transform;
            Vector3 forward = cameraTransform.forward;
            Vector3 right = cameraTransform.right;

            // 忽略y轴的影响
            forward.y = 0;
            right.y = 0;

            // 归一化向量
            forward.Normalize();
            right.Normalize();

            // 计算移动方向
            Vector3 move = (right * deltaMousePosition.x + forward * deltaMousePosition.y) * moveSpeed;

            // 更新GameObject的位置
            transform.Translate(move, Space.World);
        }

        // 更新最后的鼠标位置
        lastMousePosition = Input.mousePosition;
    }
}
