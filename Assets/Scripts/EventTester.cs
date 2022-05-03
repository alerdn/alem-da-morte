using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventTester : MonoBehaviour
{
    public UnityEvent eventCallback;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            eventCallback?.Invoke();
        }
    }
}
