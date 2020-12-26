using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public GameObject burst;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject explosion = Instantiate(burst, transform.position, Quaternion.identity);
        explosion.GetComponent<ParticleSystem>().Play();
        Destroy(gameObject);
        Destroy(explosion, 3f);
    }
}
