using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Rigidbody2D rbBall;
    float startSpeed = 300;
    public GameObject startPosition;
    private bool startGame;

    void Start()
    {
        rbBall = GetComponent<Rigidbody2D>();
        transform.position = startPosition.transform.position;
        startGame = true;
    }

    void Update()
    {
        if(startGame)
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
            transform.position = startPosition.transform.position;
            rbBall.velocity = Vector2.zero;
            startGame = true;
        }
    }

    void StartImpulse()
    {
        rbBall.AddForce(Vector2.up * startSpeed);
        startGame = false;
    }
}
