using UnityEngine;
using UnityEngine.Events;

public class MonoBehaviourAdvanceEventDispatcher : MonoBehaviour
{
    public UnityEvent onEnabledEvent = new UnityEvent();
    public UnityEvent onDisableEvent = new UnityEvent();

    private void OnEnable() => onEnabledEvent?.Invoke();
    private void OnDisable() => onDisableEvent?.Invoke();
}
