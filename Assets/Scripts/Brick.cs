using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public GameObject burst;
    private GameController gameController;

    private void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject explosion = Instantiate(burst, transform.position, Quaternion.identity);
        explosion.GetComponent<ParticleSystem>().Play();
        gameController.UpScore();
        gameController.BricksRemove();

        
        Destroy(gameObject);
        Destroy(explosion, 3f);
    }
}
