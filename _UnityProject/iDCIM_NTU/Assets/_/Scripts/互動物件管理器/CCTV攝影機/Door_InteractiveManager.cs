using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VictorDev.Managers;

public class Door_InteractiveManager : InteractiveManager
{
    protected override void AddMoreComponentToObject(Collider target)
    {
    }

    public void SetIndicatorVisible(bool isVisible)
    {
        SetOutlineVisible(isVisible);
        ///

    }
}
