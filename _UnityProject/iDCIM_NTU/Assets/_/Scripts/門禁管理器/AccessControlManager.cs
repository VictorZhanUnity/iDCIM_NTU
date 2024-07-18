using Cinemachine;
using System.Collections.Generic;
using UnityEngine;
using VictorDev.Async.CoroutineUtils;

public class AccessControlManager : MonoBehaviour
{
    [SerializeField] private CinemachineFreeLook cineCamera;
    [SerializeField] private List<LerpEmission> emissionTarget;

    private void OnEnable()
    {
        cineCamera.gameObject.SetActive(true);
        emissionTarget.ForEach(emission => emission.StartLoop());
    }

    private void OnDisable()
    {
        cineCamera.gameObject.SetActive(false);
        emissionTarget.ForEach(emission => emission.Stop());
    }
}
