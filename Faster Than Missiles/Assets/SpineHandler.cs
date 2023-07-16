using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpineHandler : MonoBehaviour
{
    public Sprite[] spineSprites;
    public SpriteRenderer spineRenderer;
    private Movement m;
    // Start is called before the first frame update
    void Start()
    {
        m = GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m.thrusting)
        {
            spineRenderer.sprite = spineSprites[1];
        }
        else
        {
            spineRenderer.sprite = null;
        }
    }
}
