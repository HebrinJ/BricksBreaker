﻿using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D rbBall;
    public float startSpeed = 500f;
    public GameObject startPosition, platform;
    
    public GameObject fallingObject;

    private bool isSlowed;
       
    void Start()
    {
        rbBall = GetComponent<Rigidbody2D>();
        transform.position = startPosition.transform.position;
    }

    void Update()
    {
        if(GameController.Instance.startGame)
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
            GameController.Instance.PlaySound(GameController.Instance.failSound);
            GameController.Instance.RemoveLive();
            transform.position = startPosition.transform.position;
            rbBall.velocity = Vector2.zero;
            GameController.Instance.startGame = true;
            PlayerControl.Instance.ResetStatus();
                        
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
        rbBall.AddForce(new Vector2(0, 1) * startSpeed);
        GameController.Instance.startGame = false;
        
    }
        
}
