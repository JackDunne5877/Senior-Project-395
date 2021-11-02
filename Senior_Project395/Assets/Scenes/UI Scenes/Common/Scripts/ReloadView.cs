using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using System;


public class ReloadView : MonoBehaviour
{
    [Serializable]
    public class ReloadEvent : UnityEvent { }

    [SerializeField]
    private ReloadEvent m_onReload = new ReloadEvent();

    public ReloadEvent onReload
    {
        get { return m_onReload; }
        set { m_onReload = value; }
    }

    public void reloadView() {
        m_onReload.Invoke();
    }
}
