using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public bool Write = false;

    private int score = 0;

    public int Score
    {
        get
        {
            if (!Write)
            {
                score = PlayerPrefs.GetInt("score");
            }
            return score;
        }
    }

    public void addScore(int point)
    {
        if (!Write)
            return;

        score += point;
        PlayerPrefs.SetInt("score", score);
    }

    void Start()
    {
        if (!Write)
        {
            GetComponent<TextMesh>().text = "GAME OVER - YOU SUCK\nPRESS TRIGGER TO TRY AGAIN\nSCORE: " + Score;
        }
    }
}
