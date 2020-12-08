using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    KeyCode openInventory = KeyCode.I;
    KeyCode goToMainMenu = KeyCode.Escape;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(openInventory))
        {
            GameManagerScript.ToggleInventory();
        }
        else if (Input.GetKeyDown(goToMainMenu))
        {
            SceneManager.LoadScene(0);
        }

    }
}
