using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugScript : MonoBehaviour
{
    GameManagerScript managerInstance;
    // Start is called before the first frame update
    void Start()
    {
        managerInstance = FindObjectOfType<GameManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DealDamage()
    {
        managerInstance.getCurCharacter().health -= 10;
        if (managerInstance.getCurCharacter().health <= 0)
        {
            Destroy(managerInstance.getCurCharacter().gameObject);
        }
    }
}
