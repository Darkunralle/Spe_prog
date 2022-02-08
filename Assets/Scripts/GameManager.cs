using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : Singleton<GameManager>
{
    private float m_elapsedTime = 0;

    [SerializeField]
    private float m_ticRate = 0.25f;

    [SerializeField]
    private GameObject m_cubePrefab;

    [SerializeField]
    private int m_boardHeight = 25;
    [SerializeField]
    private int m_boardWitdh = 8;

    private List<Cube> m_cubes = new List<Cube>();

    public enum Status
    {
        VIDE = 0,
        PLEINE = 1,
        ERROR = 10
    }
    [SerializeField]
    private Status[,] m_board;

    private void Start()
    {
        ClearBoard();
        CreateCube();
    }

    private void Update()
    {
        //Si le temps est coulé, desendre la brique
        m_elapsedTime += Time.deltaTime;
        if (m_elapsedTime >= m_ticRate)
        {
            MoveDown();
            m_elapsedTime = 0;
        }

    }

    private void ClearBoard()
    {
        m_board = new Status[m_boardWitdh,m_boardHeight];
        m_cubes.Clear();
    }



    protected override string GetSingletonName()
    {
        return "gameManager";
    }

   

    private void MoveDown()
    {
        m_elapsedTime = 0;
        for (int i = 0; i < m_cubes.Count; i++)
        {
            //le move down du script Cube (celle en pbc)
            m_cubes[i].MoveDown();
        }
    }
    public void MoveCube(int p_xOrigin, int p_yOrigin, int p_xDest, int p_yDest)
    {
        //libérer une case, remplir une autre
        m_board[p_xOrigin, p_yOrigin] = Status.VIDE;
        m_board[p_xDest, p_yDest] = Status.PLEINE;
    }

    public void CreateCube()
    {
        int xPos = Random.Range(0, m_boardWitdh);
        int ypos = m_boardHeight - 1;

        //test
        if(m_board[xPos, ypos] == Status.PLEINE)
        {
            //GameOver my guy
            Debug.Log("C'est finito");
            return;
        }
        
        //affectation
        m_board[xPos, ypos] = Status.PLEINE;

        Vector3 cubePos = new Vector3(xPos, ypos, 0);

        GameObject go = Instantiate(m_cubePrefab, cubePos, Quaternion.identity);

        Cube cube = go.GetComponent<Cube>();

        m_cubes.Add(cube);

        cube.SetPosition(xPos, ypos);
    }

    public Status GetStatus(int p_x, int p_y)
    {
        if (p_x == -1 || p_x == m_boardWitdh)
        {
            return Status.ERROR;
        }

        if (p_x < 0 || p_x >= m_boardWitdh || p_y < 0 || p_y >= m_boardHeight)
        {
            Debug.LogError("position n'existe pas");
            return Status.ERROR;
        }
        
        //Debug.Log(message: $"' position x {p_x}, position y {p_y} = {m_board[p_x, p_y]}");
        return m_board[p_x, p_y];
        
    }

   
}
