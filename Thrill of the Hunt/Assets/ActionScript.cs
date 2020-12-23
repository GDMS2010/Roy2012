using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class ActionScript : MonoBehaviour
{
    public Sprite actionImage;
    public UnityEvent action;
    public UnityEvent<GameObject> _action;
    public int remainCooldown;
    public int MaxCooldown;
    public int damage;
    public void Execute()
    {
        action.Invoke();
    }

    public void _Execute(GameObject target)
    {
        _action?.Invoke(target);
    }
}
