using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObjects : MonoBehaviour
{
    public Sprite liveSprite, expandSprite, narrowSprite, slowSprite, ammoSprite, explodeSprite;
    private SpriteRenderer spRender;
    public static ObjectTypes type;
    

    private void Start()
    {
        
        spRender = GetComponent<SpriteRenderer>();
        type = SetType();

        switch (type)
        {
            case ObjectTypes.live:
                spRender.sprite = liveSprite;
                break;
            case ObjectTypes.expand:
                spRender.sprite = expandSprite;
                break;
            case ObjectTypes.narrow:
                spRender.sprite = narrowSprite;
                break;
            case ObjectTypes.slow:
                spRender.sprite = slowSprite;
                break;
            case ObjectTypes.ammo:
                spRender.sprite = ammoSprite;
                break;
            /*case ObjectTypes.explode:
                spRender.sprite = explodeSprite;
                break;*/
            default:
                break;
        }

        Destroy(gameObject, 10f);
    }

    private void Update()
    {
        transform.Translate(Vector2.down.normalized * 2f * Time.deltaTime);
    }

    private ObjectTypes SetType()
    {
        int index = Random.Range(1, 6);

        switch (index)
        {
            case 1:
                return ObjectTypes.live;
            case 2:
                return ObjectTypes.expand;
            case 3:
                return ObjectTypes.narrow;
            case 4:
                return ObjectTypes.slow;
            case 5:
                return ObjectTypes.ammo;
            case 6:
                return ObjectTypes.explode;
            default:
                break;
        }

        return ObjectTypes.live;
    }

}

public enum ObjectTypes
{
    live,
    expand,
    narrow,
    slow,
    ammo,
    explode //эффект отключен
}