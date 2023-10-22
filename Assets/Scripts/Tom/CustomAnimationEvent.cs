using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CustomAnimationEvent : MonoBehaviour
{
    public UnityEvent eventsToCall;

    public void CallEvents()
    {
        eventsToCall.Invoke();
    }
}
