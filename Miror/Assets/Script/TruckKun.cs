using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

[RequireComponent(typeof(Rigidbody))]
public class TruckKun : NetworkBehaviour, ICar
{
    [SerializeField, Tooltip("Rigidbody du truck")]
    private Rigidbody m_myRigid;
    private float m_currentSpeed;
    private float m_weight = 26000;
    private float m_maxSpeed;
    private float m_accel;

    public float CurrentSpeed => m_currentSpeed;

    public float Weight => m_weight;

    public float MaxSpeed => m_maxSpeed;

    public float Acceleration => m_accel;

    public void Stun()
    {
        throw new System.NotImplementedException();
    }


    private void FixedUpdate()
    {
    }
}
