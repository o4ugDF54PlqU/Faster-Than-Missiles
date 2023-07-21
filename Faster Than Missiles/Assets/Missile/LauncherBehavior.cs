using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauncherBehavior : MonoBehaviour
{
    private Rigidbody2D launcherRigidBody;
    private float volleyDelay;
    private int volleyCount;
    private float burstDelay;
    private Vector3 playerPos;
    private Vector2 direction;
    private float angle;
    private Quaternion targeetRotation;
    [SerializeField] private GameObject player;
    [SerializeField] private float rotationSpeed = 90f;
    [SerializeField] private AudioClip launchSound;

    [SerializeField] private float initialVolleyDelay = 5f;
    [SerializeField] private float subsequentVolleyDelay = 10f;
    [SerializeField, Tooltip("Number of missiles fired per volley.")] private int missileCount = 1;
    [SerializeField, Tooltip("Time delay (in seconds) between shots in a volley.")] private float burstDelayRate = 2f;

    void Start()
    {
        volleyDelay = initialVolleyDelay;
        launcherRigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!player)
        {
            return;
        }
        playerPos = player.transform.position;
        direction = playerPos - transform.position;
        float angle = Vector2.SignedAngle(Vector2.up, direction);
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        launcherRigidBody.MoveRotation(Quaternion.RotateTowards(
            transform.rotation, targetRotation, rotationSpeed * Time.deltaTime));

        if (volleyDelay > 0)
            volleyDelay -= Time.deltaTime;
        else
        {
            volleyCount = missileCount;
            burstDelay = burstDelayRate;
            while (volleyCount > 0)
            {
                LaunchMissile();
                if (burstDelay > 0)
                    burstDelay -= Time.deltaTime;
                else
                {
                    LaunchMissile();
                    burstDelay = burstDelayRate;
                }
            }
            volleyDelay = subsequentVolleyDelay;
        }
    }

    void LaunchMissile()
    {
        GameObject missile = ObjectPool.SharedInstance.GetPooledObject();
        if (missile != null)
        {
            missile.transform.position = transform.position;
            missile.transform.rotation = transform.rotation;
            missile.SetActive(true);
            AudioSource.PlayClipAtPoint(launchSound, transform.position);
        }

        volleyCount--;
    }
}
