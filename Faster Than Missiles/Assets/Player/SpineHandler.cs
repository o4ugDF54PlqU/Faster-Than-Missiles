using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpineHandler : MonoBehaviour
{
    public Sprite[] spineSprites;
    public Sprite[] thrustSprites;
    public SpriteRenderer spineRenderer;
    public SpriteRenderer thrustRenderer;
    private Movement m;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        m = GetComponent<Movement>();
        rb = GetComponent<Rigidbody2D>();
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

        // handle thrust flames
        if (!m.thrusting)
        {
            thrustRenderer.sprite = null;
            return;
        }
        // get velocity in facing direction
        var forwardVelocity = Vector2.Dot(rb.velocity, transform.up);
        if (forwardVelocity > m.maxVelocity * 0.8)
        {
            thrustRenderer.sprite = thrustSprites[2];
        }
        else if (forwardVelocity > m.maxVelocity * 0.3)
        {
            thrustRenderer.sprite = thrustSprites[1];
        }
        else
        {
            thrustRenderer.sprite = thrustSprites[0];
        }
        Debug.Log(forwardVelocity);
    }
}
