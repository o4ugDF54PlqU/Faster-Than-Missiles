using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FrickinLaserBeam : MonoBehaviour
{
    public bool firing = false;
    public float laserOffset = 0.5f;
    public AudioSource audioSource;
    private LineRenderer lr;
    private Collider2D col;
    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponentInChildren<LineRenderer>();
        col = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!firing)
        {
            lr.enabled = false;
            return;
        }
        RaycastHit2D[] hits = new RaycastHit2D[10];
        int numCollidersHit = col.Raycast(transform.up, hits);
        foreach (RaycastHit2D item in hits)
        {
            if (item.collider.tag == "Wall")
            {
                lr.SetPosition(0, item.point);
                break;
            }

            if (item.collider.tag == "Launcher")
            {
                Destroy(item.collider.gameObject);
            }
        }
        lr.SetPosition(1, lr.transform.position + transform.up * laserOffset);
        lr.enabled = true;
    }

    public void Fire(InputAction.CallbackContext context)
    {
        if (context.action.IsPressed())
        {
            audioSource.Play();
            audioSource.loop = true;
            firing = true;
        }
        else
        {
            audioSource.Stop();
            audioSource.loop = false;
            firing = false;
        }
    }
}
