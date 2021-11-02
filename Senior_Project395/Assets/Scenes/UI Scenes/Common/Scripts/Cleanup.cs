using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using System;


public class Cleanup : MonoBehaviour
{
    [Serializable]
    public class CleanUpEvent : UnityEvent { }

    [SerializeField]
    private CleanUpEvent m_onCleanup = new CleanUpEvent();

    public CleanUpEvent onCleanup
    {
        get { return m_onCleanup; }
        set { m_onCleanup = value; }
    }

    // Update is called once per frame
    public void cleanupView() {
        m_onCleanup.Invoke();
    }
}
