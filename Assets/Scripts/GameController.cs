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
    public GameObject gameOverPanel;
    public Text finishScoreText, finishHighScoreText;

    private bool isGameOver = false;

    void Start()
    {
        lives = 3;
        gameOverPanel.SetActive(false);
        highScore = PlayerPrefs.GetInt("HighScore", highScore);
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

        if (lives <= 0)
        {
            isGameOver = true;
            gameOverPanel.SetActive(true);
            finishScoreText.text = "Score: " +score.ToString();

            if (highScore == 0)
            {
                highScore = score;
                PlayerPrefs.SetInt("HighScore", highScore);
            }
            else if (score > highScore)
            {
                highScore = score;
                PlayerPrefs.SetInt("HighScore", highScore);
            }

            finishHighScoreText.text = "High Score: " +highScore.ToString();

            Time.timeScale = 0;
        }
    }

    public void Restart()
    {
        Time.timeScale = 1;
        gameOverPanel.SetActive(false);
        lives = 3;
        heart1.enabled = true;
        heart2.enabled = true;
        heart3.enabled = true;
        score = 0;
        scoreText.text = score.ToString();
    }

    public void Quit()
    {
        Application.Quit();
    }
}
