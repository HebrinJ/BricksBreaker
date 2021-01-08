using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : Singleton<GameController>
{
        
    public int score;
    public int lives;
    public int highScore;
    private int scoreStep = 10;

    public Image [] hearts;
    public GameObject gameOverPanel;
    public Text scoreText, finishScoreText, finishHighScoreText, winScoreText, winHighScoreText, winGameScoreText, winGameHighScoreText;
    
    public GameObject winPanel, pausePanel, winGamePanel;
    
    private int bricksCount;
  
    public GameObject level1, level2, level3;
    public bool startGame;
    private int currentLevel;

    public GameObject playerplatform;
    
    public GameObject startPosition;

    private AudioSource audioSource;
    public AudioClip gameOverSound, winSound, collisionPlat, collisionBrick, shootSound, bonusSound, failSound;
    
    void Start()
    {
        gameOverPanel.SetActive(false);
        pausePanel.SetActive(false);
        highScore = PlayerPrefs.GetInt("HighScore", highScore);
        lives = 3;
        score = 0;
        startGame = true;
        bricksCount = level1.transform.childCount;
        currentLevel = 1;
        audioSource = GetComponent<AudioSource>();
        SetSound();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
      
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
    private void SetSound()
    {
        if (PlayerPrefs.GetInt("Sound") == 1)
            AudioListener.volume = 1;

        else if (PlayerPrefs.GetInt("Sound") == 0)
            AudioListener.volume = 0;
    }

    public void ShowUIText(string text, Text value)
    {
        value.text = text;
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
        ShowUIText("Score: " +score.ToString(), scoreText);
    }

    public void RemoveLive()
    {
        lives--;

        switch (lives)
        {
            case 2:
                hearts[2].enabled = false;
                break;
            case 1:
                hearts[1].enabled = false;
                break;
            case 0:
                hearts[0].enabled = false;
                break;
            default:
                break;
        }

        if (lives <= 0)
        {
            
            gameOverPanel.SetActive(true);
            PlaySound(gameOverSound);
            ShowUIText("Score: " + score.ToString(), finishScoreText);

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

            ShowUIText("High Score: " + highScore.ToString(), finishHighScoreText);
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
        foreach(var item in hearts)
        {
            item.enabled = true;
        }
                
        score = 0;
        startGame = true;
        ShowUIText("score: " + score.ToString(), scoreText);
        
        PlayerControl.Instance.ResetStatus();
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
            if (currentLevel < 3)
            {
                winPanel.SetActive(true);
            }
            else if (currentLevel >= 3)
            {
                winGamePanel.SetActive(true);
                ShowUIText("Score: " + score.ToString(), winGameScoreText);
                ShowUIText("High Score: " + PlayerPrefs.GetInt("HighScore").ToString(), winGameHighScoreText);
            }

            PlaySound(winSound);
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

            ShowUIText("Score: " + score.ToString(), winScoreText);
            ShowUIText("High Score: " + highScore.ToString(), winHighScoreText);
            
            PlayerControl.Instance.ResetStatus();
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

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
