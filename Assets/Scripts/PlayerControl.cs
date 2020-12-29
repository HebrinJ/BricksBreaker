using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private float horizontalMove;
    private float speed = 0.25f;
    private AudioSource audioSource;
    private GameController gameController;
    public GameObject controllerObject;

    public static bool isExplodeBall, isSlow;

        void Start()
    {
        audioSource = GetComponent<AudioSource>();
        gameController = GetComponent<GameController>();
        
    }

    void Update()
    {
        horizontalMove = Input.GetAxis("Horizontal");
        
    }

    private void FixedUpdate()
    {
        if(horizontalMove < 0)
        {
            transform.Translate(Vector2.left.normalized * speed);
        }
        else if (horizontalMove > 0)
        {
            transform.Translate(Vector2.right.normalized * speed);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Ball"))
        {
            audioSource.Play();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("FallingObject"))
        {
            ObjectTypes type = FallingObjects.type;
            TakeEffect(type);
            Destroy(collision.gameObject);
            
        }
    }

    public void TakeEffect(ObjectTypes type)
    {
        switch (type)
        {
            case ObjectTypes.live:
                if (gameController.lives == 2)
                {
                    gameController.lives++;
                    gameController.heart3.enabled = true;
                }
                else if (gameController.lives == 1)
                {
                    gameController.lives++;
                    gameController.heart2.enabled = true;
                }
                else if(gameController.lives == 0)
                {
                    gameController.lives++;
                    gameController.heart1.enabled = true;
                }
                break;
            
            case ObjectTypes.expand:
                transform.localScale = new Vector2(transform.localScale.x * 2, transform.localScale.y);
                Invoke("ResetExpand", 10f);
                break;

            case ObjectTypes.narrow:
                transform.localScale = new Vector2(transform.localScale.x / 2, transform.localScale.y);
                Invoke("ResetNarrow", 10f);
                break;

            case ObjectTypes.slow:
                isSlow = true;
                Invoke("ResetSlow", 10f);
                Debug.Log("slow");
                break;

            case ObjectTypes.ammo:
                Debug.Log("ammo");
                break;

            case ObjectTypes.explode:
                isExplodeBall = true;
                Invoke("ResetExplode", 5f);
                break;

            default:
                break;
        }
    }

    private void ResetExpand()
    {
        transform.localScale = new Vector2(transform.localScale.x / 2, transform.localScale.y);
    }

    private void ResetNarrow()
    {
        transform.localScale = new Vector2(transform.localScale.x * 2, transform.localScale.y);
    }

    private void ResetExplode()
    {
        isExplodeBall = false;
    }

    private void ResetSlow ()
    {
        isSlow = false;
    }
}
