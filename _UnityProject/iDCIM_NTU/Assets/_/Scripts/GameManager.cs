using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private CameraManager cameraManager;
    [SerializeField] private Module[] modules;


    [SerializeField] private DeviceManager deviceManager;
    [SerializeField] private WebAPIManager webAPIManager;
    private void Awake()
    {
        modules.ToList().ForEach(module =>
        {
            module.onClickModel.AddListener(cameraManager.LookAtTarget);
        });

        webAPIManager.onGetAllDCRInfo.AddListener(deviceManager.Parse_AllDCRInfo);
        webAPIManager.onGetDeviceCOBie.AddListener(deviceManager.Parse_COBie);

        deviceManager.onClickDevice.AddListener(webAPIManager.GetCOBieByDeviceId);
    }

    private void Start()
    {
        webAPIManager.GetAllDCRInfo();
    }
}
