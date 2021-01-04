using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowTextEffect : MonoBehaviour
{
    public static string spawnText;
    public GameObject spawnTextObject;
    private Text textContent;
    void Start()
    {
        textContent = spawnTextObject.GetComponentInChildren<Text>();
    }

    public void ShowText()
    {
        
        spawnTextObject.SetActive(true);
        textContent.text = spawnText;
        Invoke("ResetText", 3f);
    }

    private void ResetText()
    {
        spawnTextObject.SetActive(false);
    }
}
