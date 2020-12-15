using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
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

        SceneManager.LoadScene(destinationSceneName);

    }
}
