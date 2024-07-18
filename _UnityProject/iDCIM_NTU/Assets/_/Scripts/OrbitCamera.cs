using UnityEngine;

public class OrbitCamera : MonoBehaviour
{
    public Transform target;  // 摄像机注视的目标
    public float distance = 10.0f;  // 初始距离
    public float xSpeed = 250.0f;  // X轴旋转速度
    public float ySpeed = 120.0f;  // Y轴旋转速度
    public float scrollSpeed = 10.0f;  // 滚轮缩放速度

    public float yMinLimit = -20f;  // Y轴最小角度
    public float yMaxLimit = 80f;  // Y轴最大角度
    public float distanceMin = 3f;  // 最小距离
    public float distanceMax = 15f;  // 最大距离

    private float x = 0.0f;  // X轴角度
    private float y = 0.0f;  // Y轴角度

    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
    }

    void LateUpdate()
    {
        if (target)
        {
            // 鼠标右键控制旋转
            if (Input.GetMouseButton(1))
            {
                x += Input.GetAxis("Mouse X") * xSpeed * Time.deltaTime;
                y -= Input.GetAxis("Mouse Y") * ySpeed * Time.deltaTime;

                y = ClampAngle(y, yMinLimit, yMaxLimit);
            }

            Quaternion rotation = Quaternion.Euler(y, x, 0);

            // 滚轮控制缩放
            distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * scrollSpeed, distanceMin, distanceMax);

            // 更新摄像机位置
            Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
            Vector3 position = rotation * negDistance + target.position;

            transform.rotation = rotation;
            transform.position = position;
        }
    }

    // 限制角度的函数
    private static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}
