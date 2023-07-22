using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartButton : MonoBehaviour
{
    public Cinemachine.CinemachineTargetGroup targetGroup;
    private GameObject player;
    private bool appear = false;
    // Start is called before the first frame update
    void Awake()
    {
        LeanTween.alpha(gameObject, 0f, 0f);

        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player.activeInHierarchy || appear)
        {
            return;
        }

        appear = true;
        transform.position = player.transform.position + Vector3.up*6f;
        LeanTween.alpha(gameObject, 1f, 5f).setEaseInCubic();
        targetGroup.AddMember(transform, 1f, 1f);
    }
}
