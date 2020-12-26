using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public int score;
    public int lives;
    public int highScore;
    private int scoreStep = 10;

    public Text scoreText;
    public Image heart1, heart2, heart3;

    void Start()
    {
        lives = 3;
    }

    
    void Update()
    {
        
    }

    public void UpScore()
    {
        score += scoreStep;
        scoreText.text = "Score: " +score.ToString();
        
    }

    public void RemoveLive()
    {
        lives--;

        switch (lives)
        {
            case 2:
                heart3.enabled = false;
                break;
            case 1:
                heart2.enabled = false;
                break;
            case 0:
                heart1.enabled = false;
                break;
            default:
                break;
        }
    }
}
