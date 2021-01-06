using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    private float horizontalMove;
    public float speed = 0.25f;
    
    private AudioSource audioSource;
    public AudioClip shootingSound, collisionSound, bonusSound;

    private GameController gameController;
    public GameObject controllerObject;

    public static bool isExplodeBall, isSlow;
    public GameObject shootingPosition, bullet;
    public bool canShoot;
    private int ammo;

    ShowTextEffect textEffect;

    public bool isExpand, isNarrow;

        void Start()
    {
        audioSource = GetComponent<AudioSource>();
        gameController = controllerObject.GetComponent<GameController>();
        shootingPosition.SetActive(false);
        canShoot = false;
        textEffect = GameObject.Find("Canvas").GetComponent<ShowTextEffect>();
    }

    void Update()
    {
        horizontalMove = Input.GetAxis("Horizontal");
        
        if(canShoot)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (ammo > 0)
                {
                    Instantiate(bullet, shootingPosition.transform.position, Quaternion.identity);
                    audioSource.clip = shootingSound;
                    audioSource.Play();
                    ammo--;
                    if (ammo == 0)
                    {
                        ResetShooting();
                    }
                }
            }
        }
        
        
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
            audioSource.clip = collisionSound;
            audioSource.Play();
            
            ///Начало. Изменение направления полета мяча в зависимости от того с какой частью платформы он столкнулся
            Rigidbody2D ball = collision.gameObject.GetComponent<Rigidbody2D>();
            Vector2 hitPoint = collision.contacts[0].point;
            Vector2 platformCenter = new Vector2(transform.position.x, transform.position.y);
            ball.velocity = Vector2.zero;

            float difference = platformCenter.x - hitPoint.x;

            if(hitPoint.x < platformCenter.x)
            {
                if(isSlow)
                ball.AddForce(new Vector2((-Mathf.Abs(difference * 200)), 200));
                else if(!isSlow)
                ball.AddForce(new Vector2((-Mathf.Abs(difference * 200)), 400));
            }
            else
            {
                if(isSlow)
                ball.AddForce(new Vector2((Mathf.Abs(difference * 200)), 200));
                else if(!isSlow)
                ball.AddForce(new Vector2((Mathf.Abs(difference * 200)), 400));
            }
            ///Конец.
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("FallingObject"))
        {
            ObjectTypes type = FallingObjects.type;
            audioSource.clip = bonusSound;
            audioSource.Play();
            TakeEffect(type);
            
            Debug.Log("тип: " + type);
            
            textEffect.ShowText();

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
                ShowTextEffect.spawnText = "";
                break;
            
            case ObjectTypes.expand:
                if (!isExpand)
                {
                    transform.localScale = new Vector2(transform.localScale.x * 2, transform.localScale.y);
                    isExpand = true;
                    ShowTextEffect.spawnText = "Expand!!";
                    Invoke("ResetExpand", 10f);
                }
                break;

            case ObjectTypes.narrow:
                if (!isNarrow)
                {
                    transform.localScale = new Vector2(transform.localScale.x / 2, transform.localScale.y);
                    isNarrow = true;
                    ShowTextEffect.spawnText = "narrow...";
                    Invoke("ResetNarrow", 10f);
                }
                break;

            case ObjectTypes.slow:
                if (!isSlow)
                {
                    isSlow = true;
                    ShowTextEffect.spawnText = "Slow!";
                    Invoke("ResetSlow", 10f);
                }
                break;

            case ObjectTypes.ammo:
                shootingPosition.SetActive(true);
                ShowTextEffect.spawnText = "Shoot!!";
                canShoot = true;
                ammo = 3;
                break;

            case ObjectTypes.explode:
                isExplodeBall = true;
                Invoke("ResetExplode", 5f);
                break;

            default:
                ShowTextEffect.spawnText = "";
                break;
        }
    }

    public void ResetExpand()
    {
        transform.localScale = new Vector2(transform.localScale.x / 2, transform.localScale.y);
        isExpand = false;
    }

    public void ResetNarrow()
    {
        transform.localScale = new Vector2(transform.localScale.x * 2, transform.localScale.y);
        isNarrow = false;
    }

    public void ResetExplode()
    {
        isExplodeBall = false;
    }

    public void ResetSlow()
    {
        isSlow = false;
    }

    public void ResetShooting()
    {
        shootingPosition.SetActive(false);
        canShoot = false;
        ammo = 0;
        
    }
}
