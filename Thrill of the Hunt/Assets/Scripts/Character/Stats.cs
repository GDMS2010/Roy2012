using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Stats : MonoBehaviour
{
    [SerializeField] string characterName;
    [Header("Basic")]

    [SerializeField] int damage;
    [SerializeField] int moveSpeed;

    [Header("Attributes")]
    [SerializeField] int defense;
    [SerializeField] int nimbleness;
    [SerializeField] int brawn;
    [SerializeField] int brain;
    [SerializeField] int vigor;
    UnityEvent onHurt, onDie;

    [Header("UI")]
    [SerializeField] Sprite Portrait;

    public int getDefense() { return defense; }
    public int getNimBleness() { return nimbleness; }
    public int getBrawn() { return brawn; }
    public int getBrain() { return brain; }
    public int getVigor() { return vigor; }

    public int health;
    public int maxHealth;

    public string getCharacterName => characterName;

    private void Awake()
    {
        //Debug
        nimbleness = Random.Range(1, 20);
        GameObject.Find("TurnOrder").GetComponent<TurnOrder>().testList.Add(this.gameObject);
        maxHealth = vigor * 3;
        health = maxHealth;
    }

    public Sprite GetSprite()
    {
        return Portrait;
    }
}
