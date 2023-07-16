using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    private bool thrusting = false;
    private bool turningLeft = false;
    private bool turningRight = false;
    private Rigidbody2D playerRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
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
            transform.Rotate(Vector3.forward * 40f * Time.deltaTime);
        }
        if (turningRight)
        {
            transform.Rotate(Vector3.back * 40f * Time.deltaTime);
        }
    }

    public void Thrust(InputAction.CallbackContext context)
    {
        if (context.action.IsPressed())
        {
            Debug.Log("Thrusting");
            thrusting = true;
        }
        else
        {
            thrusting = false;
        }
    }

    public void TurnLeft(InputAction.CallbackContext context)
    {
        if (context.action.IsPressed())
        {
            Debug.Log("turning left");
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
            Debug.Log("turning right");
            turningRight = true;
        }
        else
        {
            turningRight = false;
        }
    }
}
