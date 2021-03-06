﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    
    public GameObject soundButton;
    public Sprite soundOn, soundOff;
    private Image image;
    private int audioStatus = 1;
    void Start()
    {
        audioStatus = PlayerPrefs.GetInt("Sound");
        image = soundButton.GetComponent<Image>();
        image.sprite = audioStatus == 0 ? soundOff : soundOn;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SoundSetting()
    {
        if (audioStatus == 0)
        {
            AudioListener.volume = 1;
            audioStatus = 1;
            PlayerPrefs.SetInt("Sound", audioStatus);
            image.sprite = soundOn;
            return;
        }

        if (audioStatus == 1)
        {
            AudioListener.volume = 0;
            audioStatus = 0;
            PlayerPrefs.SetInt("Sound", audioStatus);
            image.sprite = soundOff;
            return;
        }

    }
}
