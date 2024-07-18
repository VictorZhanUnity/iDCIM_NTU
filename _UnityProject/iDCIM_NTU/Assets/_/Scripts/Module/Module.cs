using UnityEngine;
using UnityEngine.Events;

public abstract class Module : MonoBehaviour
{
    public UnityEvent<Transform> onClickModel;
}
