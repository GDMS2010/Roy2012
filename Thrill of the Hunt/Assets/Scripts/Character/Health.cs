using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] int currHealth;
    [SerializeField] int maxHealth;
    bool _isAlive = true;
    [Tooltip("When character taking damage but not dying")]
    public UnityEvent onHurt;
    [Tooltip("When character taking damage and dying")]
    public UnityEvent onDie;

    public void hurt(int _amount)
    {
        currHealth -= _amount;
        if (currHealth <= 0)
        {
            _isAlive = false;
            onDie.Invoke();
        }
        else
            onHurt.Invoke();
    }

    public void heal(int _amount)
    {
        currHealth += _amount;
        if (currHealth > maxHealth)
            currHealth = maxHealth;
    }

    public bool isAlive()
    {
        return _isAlive;
    }
}
