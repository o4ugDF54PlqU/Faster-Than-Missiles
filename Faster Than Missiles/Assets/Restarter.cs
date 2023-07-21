using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Restarter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    public void Restart(InputAction.CallbackContext context)
    {
        if (context.action.IsPressed())
        {
            SceneManager.LoadScene("Level1");
        }
    }
}
