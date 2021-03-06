﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float bulletSpeed = 6f;
    public GameObject fallingObject;
    
        void Update()
    {
        transform.Translate(Vector2.up * bulletSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("GoldBrick"))
        {
            Instantiate(fallingObject, collision.transform.position, Quaternion.identity);
        }

    }
}
