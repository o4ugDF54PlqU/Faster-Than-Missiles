using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    public bool thrusting = false;
    public SpriteRenderer exhaustSprite;
    public float rotationSpeed = 180f;
    private bool turningLeft = false;
    private bool turningRight = false;
    private Rigidbody2D playerRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (thrusting)
        {
            playerRigidbody.AddForce(transform.up* 500f * Time.deltaTime);
        }
        else
        {
            
        }

        if (turningLeft)
        {
            playerRigidbody.AddTorque(rotationSpeed * Time.deltaTime);
        }
        if (turningRight)
        {
            playerRigidbody.AddTorque(-rotationSpeed * Time.deltaTime);
        }
    }

    public void Thrust(InputAction.CallbackContext context)
    {
        if (context.action.IsPressed())
        {
            thrusting = true;
            exhaustSprite.enabled = true;
        }
        else
        {
            thrusting = false;
            exhaustSprite.enabled = false;
        }
    }

    public void TurnLeft(InputAction.CallbackContext context)
    {
        if (context.action.IsPressed())
        {
            turningLeft = true;
        }
        else
        {
            turningLeft = false;
        }
    }

    public void TurnRight(InputAction.CallbackContext context)
    {
        if (context.action.IsPressed())
        {
            turningRight = true;
        }
        else
        {
            turningRight = false;
        }
    }
}
