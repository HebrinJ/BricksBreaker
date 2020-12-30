using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float bulletSpeed;
    void Start()
    {
        bulletSpeed = 0.01f;
    }

    
    void Update()
    {
        transform.Translate(Vector2.up * bulletSpeed);
    }
}
