using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    [SerializeField] private TextMeshPro m_text;
    private int m_score = 0;

    private void OnEnable()
    {
        Cube.onScore += HandleScore;
    }

    private void OnDisable()
    {
        Cube.onScore -= HandleScore;
    }

    private void HandleScore(int p_score)
    {
        m_score += p_score;
        m_text.text = m_score.ToString();
        // Maj le txt
    }
}
