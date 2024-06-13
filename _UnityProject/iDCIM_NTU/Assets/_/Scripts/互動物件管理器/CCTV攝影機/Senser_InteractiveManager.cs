using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VictorDev.Managers;

public class Senser_InteractiveManager : InteractiveManager
{
    protected override void AddMoreComponentToObject(Collider target)
    {
    }

    public void SetIndicatorVisible(bool isVisible)
    {
        SetOutlineVisible(isVisible);
    }
}
