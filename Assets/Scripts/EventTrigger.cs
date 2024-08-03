using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventTrigger : MonoBehaviour
{
    public UnityEvent EventsToActivate;
    public UnityEvent EventsToDeactivate;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player activates objects");
            EventsToActivate.Invoke();
            EventsToDeactivate.Invoke();
        }
    }
}
