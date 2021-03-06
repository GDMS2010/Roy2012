﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonTest : MonoBehaviour
{

    [SerializeField]
    private string destinationSceneName = "";


    public void PressMe()
    {
        Debug.Log("I wa pressed!");
    }

    public void GoToScene()
    {
#if UNITY_EDITOR 
        UnityEditor.SceneManagement.EditorSceneManager.LoadScene(destinationSceneName);
#else
        SceneManager.LoadScene(destinationSceneName);
#endif
    }
}
