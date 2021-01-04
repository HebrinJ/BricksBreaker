using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float bulletSpeed;
    public GameObject fallingObject;
    void Start()
    {
        bulletSpeed = 0.01f;
    }

    
    void Update()
    {
        transform.Translate(Vector2.up * bulletSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("GoldBrick"))
        {
            Instantiate(fallingObject, collision.transform.position, Quaternion.identity);

        }

    }
}
