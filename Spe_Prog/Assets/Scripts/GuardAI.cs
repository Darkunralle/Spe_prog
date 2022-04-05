using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class GuardAI : MonoBehaviour
{
    [SerializeField, Tooltip("points de patrouilles")]
    private List<Transform> m_waypointList;

    [SerializeField, Tooltip("le nav mesh")]
    private NavMeshAgent m_agentAI;


    private Coroutine m_stateManagementCor;

    private enum State
    {
        PATROUILLE,
        CHASSE
    }

    private State m_currentStatus = State.PATROUILLE;

    [SerializeField, Tooltip("index des waypoints")] private int m_indexWaypoint;

    [SerializeField, Tooltip("appele du delegate")] private Event m_triggeredEvent;

    private void OnEnable()
    {
        m_agentAI = GetComponent<NavMeshAgent>();
        m_triggeredEvent.onTriggered += HandleTriggerEvent;


        m_stateManagementCor = StartCoroutine(StateManagement());

    }

    private void OnDisable()
    {
        if (m_stateManagementCor != null)
        {
            StopCoroutine(m_stateManagementCor);
            m_stateManagementCor = null;
        }
        
        m_triggeredEvent.onTriggered += HandleTriggerEvent;
    }
    
    private void HandleTriggerEvent(Vector3 p_position)
    {
        m_currentStatus = State.CHASSE;
        m_agentAI.destination = p_position;
        
    }


    private void Update()
    {
        
        if (m_currentStatus == State.PATROUILLE)
        {
            m_agentAI.SetDestination(m_waypointList[m_indexWaypoint].position);
            CheckPos();
        }
    }


    IEnumerator StateManagement()
    {
        // Code Logic

        while (m_currentStatus == State.CHASSE)
        {
            yield return new  WaitForSeconds(1);
            Debug.Log("1 sec pass");
        }

    }

    void CheckPos()
    {
        if (Mathf.Approximately(transform.position.x , m_waypointList[m_indexWaypoint].position.x)  && Mathf.Approximately(transform.position.z, m_waypointList[m_indexWaypoint].position.z))
        {
            UpdatePos();
        }
    }

    void UpdatePos()
    {
        m_indexWaypoint++;
        if (m_indexWaypoint > m_waypointList.Count - 1)
        {
            m_indexWaypoint = 0;
        }
    }
}