using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.PlasticSCM.Editor.WebApi;

public class WinHandler : MonoBehaviour
{
    private bool active = true;
    private bool danger;

    public TMP_Text win_text;
    public TMP_Text threat_text;

    private int currentThreats;
    private int maxThreats;
    // Start is called before the first frame update
    void Start()
    {
        danger = (GameObject.FindWithTag("Launcher") != null);
        win_text.text = "";

        maxThreats = GameObject.FindGameObjectsWithTag("Launcher").Length;
        currentThreats = GameObject.FindGameObjectsWithTag("Launcher").Length;

        threat_text.text = "Active Threats " + currentThreats.ToString() + " / " + maxThreats.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        danger = (GameObject.FindWithTag("Launcher") != null);

        if (!danger && active) 
        {
            active = false;
            win_text.text = "WIN!";
        } else
        {
            currentThreats = GameObject.FindGameObjectsWithTag("Launcher").Length;
            threat_text.text = "Active Threats " + currentThreats.ToString() + " / " + maxThreats.ToString();
        }
    }
}
