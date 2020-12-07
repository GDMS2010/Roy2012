using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class ActionScript : MonoBehaviour
{
    public Sprite actionImage;
    public UnityEvent action;
    public void Execute()
    {
        action.Invoke();
    }
}
