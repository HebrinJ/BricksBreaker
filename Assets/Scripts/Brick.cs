using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public GameObject burst, explode;
    private GameController gameController;
    //private AudioSource audioSource;

    private void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        //audioSource = GetComponent<AudioSource>();
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject explosion = Instantiate(burst, transform.position, Quaternion.identity);
        explosion.GetComponent<ParticleSystem>().Play();
        gameController.UpScore();
        gameController.BricksRemove();
        
        /* Эффект взрыва отключен
         * 
        if (PlayerControl.isExplodeBall == true)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.8f);
            audioSource.Play();
            Debug.Log("buh");
            
            foreach (var item in colliders)
            {
                if (item.CompareTag("Ball"))
                    continue;

                gameController.BricksRemove();
                Destroy(item.gameObject);
            }
            GameObject massExplode = Instantiate(explode, transform.position, Quaternion.identity);
            massExplode.GetComponent<ParticleSystem>().Play();
            
        }*/

        Destroy(gameObject);
        Destroy(explosion, 3f);
    }

}
