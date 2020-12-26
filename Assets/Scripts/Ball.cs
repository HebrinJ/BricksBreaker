using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Rigidbody2D rbBall;
    float startSpeed = 300;
    public GameObject startPosition;
    void Start()
    {
        rbBall = GetComponent<Rigidbody2D>();

        StartImpulse();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Ground"))
        {
            transform.position = startPosition.transform.position;
            rbBall.velocity = Vector2.zero;
                
        }
    }

    void StartImpulse()
    {
        rbBall.AddForce(Vector2.up * startSpeed);
    }
}
