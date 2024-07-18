using UnityEngine;

public class SO_Sensor : ScriptableObject
{
    [SerializeField] private float rtValue;

    public float RT_Value => rtValue;
}
