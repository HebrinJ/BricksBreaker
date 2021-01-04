using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    public Text winScoreText, winHighScoreText;

    public GameObject winPanel;
    public GameObject pausePanel;

    private int bricksCount;
  
    public GameObject level1, level2, level3;
    public bool startGame;
    private int currentLevel;

    public GameObject playerplatform;
    private PlayerControl playerControl;

    public GameObject startPosition;

    private AudioListener audio;
    private AudioSource audioSource;
    public AudioClip gameOverSound, winSound;

    void Start()
    {
        gameOverPanel.SetActive(false);
        pausePanel.SetActive(false);
        highScore = PlayerPrefs.GetInt("HighScore", highScore);
        lives = 3;
        score = 0;
        startGame = true;
        bricksCount = level1.transform.childCount;
        playerControl = playerplatform.GetComponent<PlayerControl>();
        currentLevel = 1;
        audioSource = GetComponent<AudioSource>();
        audio = GetComponent<AudioListener>();
        SetSound();

    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
      
    }

    private void SetSound()
    {
        if (PlayerPrefs.GetInt("Sound") == 1)
            audio.enabled = true;

        else if (PlayerPrefs.GetInt("Sound") == 0)
            audio.enabled = false;
    }

    private void Pause()
    {
        Time.timeScale = 0;
        
        pausePanel.SetActive(true);
        
    }

    public void Resume()
    {
        
        Time.timeScale = 1;
        pausePanel.SetActive(false);
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
            
            gameOverPanel.SetActive(true);
            audioSource.clip = gameOverSound;
            audioSource.Play();
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
        currentLevel = 1;
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
        gameOverPanel.SetActive(false);
        winPanel.SetActive(false);
        lives = 3;
        heart1.enabled = true;
        heart2.enabled = true;
        heart3.enabled = true;
        score = 0;
        startGame = true;
        scoreText.text = "score: " +score.ToString();
        playerControl.ResetShooting();
    }

    public void Quit()
    {
        Application.Quit();
    }

    

    public void BricksRemove()
    {
        bricksCount--;
        

        if (bricksCount == 0)
        {
            winPanel.SetActive(true);
            audioSource.clip = winSound;
            audioSource.Play();
            Time.timeScale = 0;
            
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

            winScoreText.text = "Score: " + score;
            winHighScoreText.text = "High Score: " + highScore.ToString();
        }
    }

    public void ChooseNextLevel()
    {
        Time.timeScale = 1;
        LoadLevel(++currentLevel);

    }

    void LoadLevel(int currentLevel)
    {
        if(currentLevel == 1)
        {
            level1.SetActive(true);
            level2.SetActive(false);
            level3.SetActive(false);
            bricksCount = level1.transform.childCount;
            winPanel.SetActive(false);
        }
        else if(currentLevel == 2)
        {
            level1.SetActive(false);
            level2.SetActive(true);
            level3.SetActive(false);
            bricksCount = level2.transform.childCount;
            winPanel.SetActive(false);
            GameObject ball = GameObject.Find("Ball");
            ball.transform.position = startPosition.transform.position;
            Rigidbody2D rbBall = ball.GetComponent<Rigidbody2D>();
            rbBall.velocity = Vector2.zero;
            startGame = true;
        }
        else if(currentLevel == 3)
        {
            level1.SetActive(false);
            level2.SetActive(false);
            level3.SetActive(true);
            bricksCount = level3.transform.childCount;
            winPanel.SetActive(false);
            GameObject ball = GameObject.Find("Ball");
            ball.transform.position = startPosition.transform.position;
            Rigidbody2D rbBall = ball.GetComponent<Rigidbody2D>();
            rbBall.velocity = Vector2.zero;
            startGame = true;
        }
        else
        {
            Restart();
        }
    }
}
