using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : Singleton<PlayerControl>
{
    private float horizontalMove;
    public float speed = 0.25f;

    private float time;
    private bool isTimerOn;

    public GameObject controllerObject;

    public static bool isExplodeBall, isSlow;
    public GameObject shootingPosition, bullet;
    public bool canShoot;
    private int ammo;

    ShowTextEffect textEffect;

    void Start()
    {
        shootingPosition.SetActive(false);
        canShoot = false;
        isTimerOn = false;
        textEffect = GameObject.Find("Canvas").GetComponent<ShowTextEffect>();
        time = 0f;
    }

    void Update()
    {
        horizontalMove = Input.GetAxis("Horizontal");

        if (canShoot)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (ammo > 0)
                {
                    Instantiate(bullet, shootingPosition.transform.position, Quaternion.identity);
                    GameController.Instance.PlaySound(GameController.Instance.shootSound);
                    ammo--;
                    if (ammo == 0)
                    {
                        ResetStatus();
                    }
                }
            }
        }

        if (time > 0 && isTimerOn)
        {
            time -= Time.deltaTime;
            
        }
        else if (time <= 0 && isTimerOn)
        {
            ResetStatus();
            isTimerOn = false;
        }
    }

    private void FixedUpdate()
    {
        if (horizontalMove < 0)
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
        if (collision.collider.CompareTag("Ball"))
        {
            GameController.Instance.PlaySound(GameController.Instance.collisionPlat);
            
            ///Начало. Изменение направления полета мяча в зависимости от того с какой частью платформы он столкнулся
            Rigidbody2D ball = collision.gameObject.GetComponent<Rigidbody2D>();
            Vector2 hitPoint = collision.contacts[0].point;
            Vector2 platformCenter = new Vector2(transform.position.x, transform.position.y);
            ball.velocity = Vector2.zero;

            float difference = platformCenter.x - hitPoint.x;

            if (hitPoint.x < platformCenter.x)
            {
                if (isSlow)
                    ball.AddForce(new Vector2((-Mathf.Abs(difference * 200)), 200));
                else if (!isSlow)
                    ball.AddForce(new Vector2((-Mathf.Abs(difference * 200)), 400));
            }
            else
            {
                if (isSlow)
                    ball.AddForce(new Vector2((Mathf.Abs(difference * 200)), 200));
                else if (!isSlow)
                    ball.AddForce(new Vector2((Mathf.Abs(difference * 200)), 400));
            }
            ///Конец.
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("FallingObject"))
        {
            ObjectTypes type = FallingObjects.type;
            GameController.Instance.PlaySound(GameController.Instance.bonusSound);
            TakeStatus(type);
            textEffect.ShowText();
            Destroy(collision.gameObject);

        }
    }

    public void TakeStatus(ObjectTypes type)
    {
        ShowTextEffect.spawnText = "";
        ResetStatus();

        switch (type)
        {
            case ObjectTypes.live:
                
                foreach (var item in GameController.Instance.hearts)
                {
                    if (item.enabled)
                    {
                        continue;
                    }
                    else
                    {
                        GameController.Instance.lives++;
                        item.enabled = true;

                        if (GameController.Instance.lives > 3)
                            GameController.Instance.lives = 3;
                    }
                }

                break;

            case ObjectTypes.expand:
                transform.localScale = new Vector2(transform.localScale.x * 2, transform.localScale.y);
                ShowTextEffect.spawnText = "Expand!!";
                isTimerOn = true;
                time = 10;
                break;

            case ObjectTypes.narrow:
                transform.localScale = new Vector2(transform.localScale.x / 2, transform.localScale.y);
                ShowTextEffect.spawnText = "narrow...";
                isTimerOn = true;
                time = 10;
                break;

            case ObjectTypes.slow:
                if (!isSlow)
                {
                    isSlow = true;
                    ShowTextEffect.spawnText = "Slow!";
                }
                isTimerOn = true;
                time = 10;
                break;

            case ObjectTypes.ammo:
                shootingPosition.SetActive(true);
                ShowTextEffect.spawnText = "Shoot!!";
                canShoot = true;
                ammo = 3;
                break;

            /*case ObjectTypes.explode:
                isExplodeBall = true;
                Invoke("ResetExplode", 5f);
                break;*/

            default:
                break;
        }
    }

    public void ResetStatus()
    {
        transform.localScale = new Vector2(0.6f, 0.6f);
        
        shootingPosition.SetActive(false);
        canShoot = false;
        ammo = 0;

        isSlow = false;
        
        isTimerOn = false;
        time = 0;
    }
    
}
