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
    public GameObject fallingObject;

    private bool isSlowed;
   
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

        if(PlayerControl.isSlow && !isSlowed)
        {
            rbBall.velocity = new Vector2(rbBall.velocity.x / 2, rbBall.velocity.y / 2);
            isSlowed = true;
        }
        else if(!PlayerControl.isSlow && isSlowed)
        {
            rbBall.velocity = new Vector2(rbBall.velocity.x * 2, rbBall.velocity.y * 2);
            isSlowed = false;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("GoldBrick"))
        {
            Instantiate(fallingObject, collision.transform.position, Quaternion.identity);
            
        }
    }

    public void StartImpulse()
    {
        rbBall.AddForce(new Vector2(Random.Range(-1.5f, 1.5f), 1) * startSpeed);
        gameController.startGame = false;

    }

    
}
