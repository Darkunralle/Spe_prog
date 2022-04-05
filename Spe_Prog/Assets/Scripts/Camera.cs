using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField, Tooltip("trigger de l'event")] private Event m_triggeredEvent;

    [SerializeField, Tooltip("recup le layer du player")] private LayerMask m_layerPlayer;

    private void OnTriggerEnter(Collider other)
    {
        if ((m_layerPlayer.value & (1 << other.gameObject.layer)) > 0)
        {
            m_triggeredEvent.Raise(other.gameObject.transform.position);
        }
    }
}
