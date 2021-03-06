﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        managerInstance.getCurCharacter().hurt(10, Stats.DamageType.True);
        if (!managerInstance.getCurCharacter().isAlive())
        {
            Destroy(managerInstance.getCurCharacter().gameObject);
        }
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(2);
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(3);
    }
}
