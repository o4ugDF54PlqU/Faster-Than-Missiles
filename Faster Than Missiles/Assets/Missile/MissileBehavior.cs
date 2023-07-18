using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class MissileBehavior : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private Vector3 playerPos;
    [Space]

    private Rigidbody2D missileRigidBody;
    [Header("Missile Movement Variables")]
    [SerializeField] private float thrustForce = 1000;
    [SerializeField] private float maxVelocity = 10;
    [SerializeField] private float passiveDrag = 1;
    [SerializeField] private float activeDrag = 0.3f;
    [SerializeField] private float passiveAngularDrag = 100;
    [SerializeField] private float activeAngularDrag = 0.01f;
    [SerializeField] private float fuelTime = 999f;
    [SerializeField] private float rotationSpeed = 180f;
    private Quaternion toRotation;

    void Start()
    {
        missileRigidBody = GetComponent<Rigidbody2D>();

        missileRigidBody.drag = passiveDrag;
        missileRigidBody.angularDrag = passiveAngularDrag;
    }

    void Update()
    {
        playerPos = player.transform.position;
        // Debug.Log(playerPos);

        if (fuelTime > 0)
        {
            missileRigidBody.AddForce(transform.up * thrustForce * Time.deltaTime);

            Vector2 direction = playerPos - transform.position;
            float angle = Vector2.SignedAngle(Vector2.up, direction);
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
            Debug.Log(angle);
            missileRigidBody.MoveRotation(Quaternion.RotateTowards(
                transform.rotation, targetRotation, rotationSpeed * Time.deltaTime));

            // fuelTime -= 1f * Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        if (missileRigidBody.velocity.magnitude > maxVelocity)
            missileRigidBody.velocity = missileRigidBody.velocity.normalized * maxVelocity;

        // if (Mathf.Abs(missileRigidBody.angularVelocity) > maxAngularVelocity)
        // {
        //     if (missileRigidBody.angularVelocity > 0)
        //         missileRigidBody.angularVelocity = maxAngularVelocity;
        //     else
        //         missileRigidBody.angularVelocity = -maxAngularVelocity;
        // }

        // if (fuelTime > 0)
        // {
        //     missileRigidBody.drag = activeDrag;
        //     missileRigidBody.angularDrag = activeAngularDrag;
        // }
        // else
        // {
        //     missileRigidBody.drag = passiveDrag;
        //     missileRigidBody.angularDrag = passiveAngularDrag;
        // }

    }
}
