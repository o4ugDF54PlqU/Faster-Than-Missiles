using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MissileBehavior : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private Vector2 playerPos;

    void Update()
    {
        playerPos = player.transform.position;
    }
}
