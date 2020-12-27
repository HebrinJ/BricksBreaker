using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Rigidbody2D rbBall;
    float startSpeed = 280;
    public GameObject startPosition, platform;
    private PolygonCollider2D platformBox;
    private AudioSource audioSource;
    private GameController gameController;
   
    void Start()
    {
        rbBall = GetComponent<Rigidbody2D>();
        platformBox = platform.GetComponent<PolygonCollider2D>();
        audioSource = GetComponent<AudioSource>();
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        transform.position = startPosition.transform.position;
        
    }

    void Update()
    {
        if(gameController.startGame)
        {
            transform.position = startPosition.transform.position;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartImpulse();
                
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Ground"))
        {
            audioSource.Play();
            gameController.RemoveLive();
            transform.position = startPosition.transform.position;
            rbBall.velocity = Vector2.zero;
            gameController.startGame = true;
        }
    }

    void StartImpulse()
    {
        rbBall.AddForce(new Vector2(Random.Range(-1.5f, 1.5f), 1) * startSpeed);
        gameController.startGame = false;

    }

    
}
