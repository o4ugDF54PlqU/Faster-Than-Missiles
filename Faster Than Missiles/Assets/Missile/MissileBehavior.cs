using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class MissileBehavior : MonoBehaviour
{
    public GameObject player;
    public Cinemachine.CinemachineTargetGroup cameraFollowGroup;
    private Vector3 playerPos;
    [SerializeField] private AudioClip boomSound;
    [Space]

    private Rigidbody2D missileRigidBody;
    [Header("Missile Movement Variables")]
    [SerializeField] private float initialThrust = 1000f;
    [SerializeField] private float thrustForce = 1000;
    [SerializeField] private float maxVelocity = 10;
    [SerializeField] private float passiveDrag = 1;
    [SerializeField] private float activeDrag = 0.3f;
    [SerializeField] private float passiveAngularDrag = 100;
    [SerializeField] private float activeAngularDrag = 0.01f;
    [SerializeField] private float fuelTime = 1.5f;
    [SerializeField] private float rotationSpeed = 180f;
    [SerializeField] private float cameraGroupWeight = 1f;
    private Quaternion toRotation;

    void Start()
    {
        missileRigidBody = GetComponent<Rigidbody2D>();
        missileRigidBody.AddForce(transform.up * initialThrust);

        missileRigidBody.drag = passiveDrag;
        missileRigidBody.angularDrag = passiveAngularDrag;

        cameraFollowGroup = FindAnyObjectByType<Cinemachine.CinemachineTargetGroup>();
        cameraFollowGroup.AddMember(transform, cameraGroupWeight, 1f);
    }

    void Update()
    {
        if (fuelTime >= 0)
        {
            fuelTime -= 1f * Time.deltaTime; 
            return;
        }
        playerPos = player.transform.position;
        var thrust = Random.Range(thrustForce * 0.5f, thrustForce * 1.5f);
        missileRigidBody.AddForce(transform.up * thrust * Time.deltaTime);

        Vector2 direction = playerPos - transform.position;
        float angle = Vector2.SignedAngle(Vector2.up, direction);
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        missileRigidBody.MoveRotation(Quaternion.RotateTowards(
            transform.rotation, targetRotation, rotationSpeed * Time.deltaTime));
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == player)
        {
            cameraFollowGroup.RemoveMember(transform);
            player.SetActive(false);
        }


        if (collision.gameObject.tag == "Missile")
        {
            return;
        }

        AudioSource.PlayClipAtPoint(boomSound, transform.position);
        gameObject.SetActive(false);
    }
}
