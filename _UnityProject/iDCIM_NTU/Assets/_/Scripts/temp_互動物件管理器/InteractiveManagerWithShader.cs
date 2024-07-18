using System.Collections.Generic;
using UnityEngine;
using VictorDev.Managers;

public class InteractiveManagerWithShader : InteractiveManager
{
    private Dictionary<Transform, Material> materialDict { get; set; } = new Dictionary<Transform, Material>();

    protected override void AddMoreComponentToObject(Collider target)
    {
        if (target.TryGetComponent<MeshRenderer>(out MeshRenderer meshRenderer))
        {
            materialDict[target.transform] = meshRenderer.material;
        }
        else
        {
            materialDict[target.transform] = target.transform.GetChild(0).GetComponent<MeshRenderer>().sharedMaterial;
        }
    }

    public virtual void SetIndicatorVisible(bool isVisible)
    {
        SetOutlineVisible(isVisible);
        foreach (Material material in materialDict.Values)
        {
            material.SetInt("_IsActivated", (isVisible ? 1 : 0));
            material.SetInt("_IsSelected", 0);
        }
    }

    protected void SetShaderVisible(Transform targetTrans)
    {
        SetIndicatorVisible(true);
        materialDict[targetTrans].SetInt("_IsSelected", 1);
    }

    private void OnApplicationQuit()
    {
        SetIndicatorVisible(false);
    }
}
