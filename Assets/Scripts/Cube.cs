using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    private int m_cubePosX;
    private int m_cubePosY;

    private float m_elapsedTime = 0;

    [SerializeField]
    private float m_ticRate = 0.25f;

    private float m_leftOrRight;

    private bool m_isLocked = false;
    private bool m_cantMove = false;

    public delegate void DeleScore(int p_score);
    public static DeleScore onScore;

    private void OnEnable()
    {
        GameManager.Instance.onMove += HandleMove;
        GameManager.Instance.onStraf += HandleStraf;
    }

    private void OnDisable()
    {
        GameManager.Instance.onMove -= HandleMove;
        GameManager.Instance.onStraf -= HandleStraf;
    }

    private void HandleStraf()
    {
        m_leftOrRight = Input.GetAxisRaw("Horizontal");

        if (m_cantMove)
        {
            return;

        }

        if (GameManager.Instance.GetStatus(m_cubePosX + (int)m_leftOrRight, m_cubePosY) == GameManager.Status.VIDE)
        {
            GameManager.Instance.MoveCube(m_cubePosX, m_cubePosY, m_cubePosX + (int)m_leftOrRight, m_cubePosY);
            m_cubePosX += (int)m_leftOrRight;
            transform.position = new Vector3(m_cubePosX, m_cubePosY, 0);

        }
    }
    public void HandleMove()
    {
        if (m_isLocked)
        {
            Debug.Log(message: "Le cube est bloqué", this);
            //verifier si ligne du bas libérée ?
            
            return;
        }
        //si la case en dessous est vide, descendre cube
        //Si en bas du tableau
        //sinon lock cube
        
        if (m_cubePosY == 0 || GameManager.Instance.GetStatus(m_cubePosX, m_cubePosY - 1) == GameManager.Status.PLEINE)
        {
            Debug.Log(message: "on bloque le cube", this);
            m_isLocked = true;
            m_cantMove = true;
            onScore?.Invoke(10);
            GameManager.Instance.CreateCube();
            return;
        }
        
        GameManager.Instance.MoveCube(m_cubePosX, m_cubePosY, m_cubePosX, --m_cubePosY);
        transform.position = new Vector3(m_cubePosX, m_cubePosY, 0);
    }

    public void SetPosition(int p_x , int p_y)
    {
        m_cubePosX = p_x;
        m_cubePosY = p_y;
    }
}
