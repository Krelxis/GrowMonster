using UnityEngine;
using UnityEngine.Events;

public class TriggerEvent : MonoBehaviour
{
    [SerializeField] private UnityEvent _events; 

    public void CallEvents()
    {
        _events.Invoke();
    }
}
