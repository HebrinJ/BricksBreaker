using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private AudioListener audio;
    public GameObject soundButton;
    public Sprite soundOn, soundOff;
    private Image image;
    private int audioStatus = 1;
    void Start()
    {
        audio = GetComponent<AudioListener>();
        audioStatus = PlayerPrefs.GetInt("Sound");
        image = soundButton.GetComponent<Image>();
        SoundSetting();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SoundSetting()
    {
        if (audioStatus == 0)
        {
            audio.enabled = true;
            audioStatus = 1;
            PlayerPrefs.SetInt("Sound", audioStatus);
            image.sprite = soundOn;
            return;
        }

        if (audioStatus == 1)
        {
            audio.enabled = false;
            audioStatus = 0;
            PlayerPrefs.SetInt("Sound", audioStatus);
            image.sprite = soundOff;
            return;
        }

    }
}
