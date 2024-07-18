using Cinemachine;
using UnityEngine;
using VictorDev.CameraHandler;

public class CameraManager : MonoBehaviour
{
    [Header(">>> Cinemachine Free Look±±¨î¾¹")]
    [SerializeField] private CinemachineFreeLook cinemachineCamera;
    [SerializeField] private ZoomWithMouseWheel zoomController;
    [SerializeField] private Transform lookAtTarget;

    public void LookAtTarget(Transform target)
    {
        lookAtTarget.position = target.position;
        zoomController.OrbitRadius = 2.5f;
    }

    private void OnValidate()
    {
        cinemachineCamera ??= transform.GetChild(0).GetComponent<CinemachineFreeLook>();
        zoomController ??= cinemachineCamera.GetComponent<ZoomWithMouseWheel>();
        lookAtTarget ??= cinemachineCamera.LookAt;
    }
}
