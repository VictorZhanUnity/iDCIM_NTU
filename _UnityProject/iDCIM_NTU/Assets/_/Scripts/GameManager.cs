using System.IO;
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
        //webAPIManager.GetAllDCRInfo();
        //暫時測試用
        webAPIManager.onGetAllDCRInfo.Invoke(GetTempJsonText());
    }

    private string GetTempJsonText()
    {
        string path = "Assets/Resources/tempJson.json";
        if (File.Exists(path))
        {
            string fileContent = "";

            using (StreamReader reader = new StreamReader(path))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    fileContent += line + "\n";
                }
            }
            Debug.Log(fileContent);
            return fileContent;
        }
        else
        {
            Debug.LogError("檔案不存在！");
            return "檔案不存在！";
        }
    }
}
