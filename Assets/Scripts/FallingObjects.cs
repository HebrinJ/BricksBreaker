using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObjects : MonoBehaviour
{
    public Sprite liveSprite, expandSprite;
    private SpriteRenderer spRender;
    private ObjectTypes type;

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
            default:
                break;
        }

        Destroy(gameObject, 10f);
    }

    private void Update()
    {
        transform.Translate(Vector2.down.normalized * 0.005f);
    }

    private ObjectTypes SetType()
    {
        int index = Random.Range(1, 3);

        switch (index)
        {
            case 1:
                return ObjectTypes.live;
            case 2:
                return ObjectTypes.expand;
            default:
                break;
        }

        return ObjectTypes.live;
    }


}

enum ObjectTypes
{
    live,
    expand
}