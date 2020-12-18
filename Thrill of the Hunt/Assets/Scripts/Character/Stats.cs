﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Stats : MonoBehaviour
{
    [SerializeField] string characterName;
    [Header("Basic")]
    public int currHealth;
    public int maxHealth;
    [SerializeField] int damage;
    [SerializeField] int attackRange;
    [SerializeField] int moveSpeed;
    [SerializeField] int lv;

    [Header("Attributes")]
    [SerializeField] int defense;
    [SerializeField] int nimbleness;
    [SerializeField] int brawn;
    [SerializeField] int brain;
    [SerializeField] int vigor;
    public int initiative;

    [Header("EnemyOnly")]
    [SerializeField] GameObject loot;
    [SerializeField] int exp;

    public UnityEvent onHurt, onDie, onHeal;

    [Header("UI")]
    [SerializeField] Sprite Portrait;

    public int getDefense => defense;
    public int getNimBleness => nimbleness;
    public int getBrawn => brawn;
    public int getBrain => brain;
    public int getVigor => vigor;
    public int getMoveSpeed => moveSpeed;
    public int getDamage => damage;
    public int getAttackRange => attackRange;

    bool _isAlive = true;

    public string getCharacterName => characterName;

    private void Awake()
    {
        //Debug
        initiative = Random.Range(1, 20) + nimbleness;
        GameObject.Find("TurnOrder").GetComponent<TurnOrder>().testList.Add(this.gameObject);
        maxHealth = vigor * 3;
        currHealth = maxHealth;
        moveSpeed = nimbleness;
    }

    public Sprite GetSprite()
    {
        return Portrait;
    }

    #region Health
    public enum DamageType
    {
        True, // Ignore armor
        Normal // Protect from armor
    }
    public void hurt(int _amount, DamageType type)
    {
        int receivedDmg;
        switch (type)
        {
            case DamageType.True:
                receivedDmg = _amount;
                break;
            case DamageType.Normal:
                receivedDmg = _amount - defense;
                if (receivedDmg < 0) receivedDmg = 0;
                break;
            default:
                receivedDmg = _amount;
                break;
        }
        currHealth -= receivedDmg;
        if (currHealth <= 0)
        {
            _isAlive = false;
            onDie?.Invoke();
            FindObjectOfType<GameManagerScript>().GetTurnOrder().removeCharacter(this.gameObject);
            gameObject.SetActive(false);
        }
        else
            onHurt?.Invoke();
    }

    public void heal(int _amount)
    {
        currHealth += _amount;
        if (currHealth > maxHealth)
            currHealth = maxHealth;
        onHeal?.Invoke();
    }

    public bool isAlive()
    {
        return _isAlive;
    }
    //Call when modify any stats
    public void modifyStats(statsData data)
    {

    }

    public class statsData
    {
        public int maxHP { get; set; }
        public int damage { get; set; }
        public int attackRange { get; set; }
        public int moveSpeed { get; set; }

        public int defense { get; set; }
        public int nimbleness { get; set; }
        public int brawn { get; set; }
        public int brain { get; set; }
        public int vigor { get; set; }
    }

    #endregion
}
