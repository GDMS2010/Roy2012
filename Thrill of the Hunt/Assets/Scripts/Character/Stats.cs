using System.Collections;
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
    public int lv;

    public Leveling leveling;
    public int statpoint;
    public Attribute[] attributes;

    public InventoryObject inventory;
    public InventoryObject equipment;

    [Header("Attributes")]
    public int defense;
    public int nimbleness;
    public int brawn;
    public int brain;
    public int vigor;
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
        leveling = new Leveling(1, OnLevelUp);

        for (int i = 0; i < equipment.GetSlots.Length; i++)
        {
            equipment.GetSlots[i].OnBeforeUpdate += OnBeforeSlotUpdate;
            equipment.GetSlots[i].OnAfterUpdate += OnAfterSlotUpdate;
        }
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
        currHealth += data.maxHP;
        maxHealth += data.maxHP;

        damage += data.damage;
        attackRange += data.attackRange;
        moveSpeed += data.moveSpeed;

        defense += data.defense;
        nimbleness += data.nimbleness;
        brawn += data.brawn;
        brain += data.brain;
        vigor += data.vigor;
    }

    public void AttributeModified(Attribute attribute)
    {
        Debug.Log(string.Concat(attribute.type, "was updated! Value is now ", attribute.value.ModifiedValue));
    }

    public void OnBeforeSlotUpdate(InventorySlot _slot)
    {
        if (_slot.ItemObject == null)
            return;
        switch (_slot.parent.inventory.type)
        {
            case InterfaceType.Inventory:
                break;
            case InterfaceType.Equipment:
                print(string.Concat("removed ", _slot.ItemObject, " on ", _slot.parent.inventory.type, ", Allowed Items: ", string.Join(", ", _slot.AllowedItems)));

                for (int i = 0; i < _slot.item.buffs.Length; i++)
                {
                    for (int j = 0; j < attributes.Length; j++)
                    {
                        if (attributes[j].type == _slot.item.buffs[i].attributes)
                        {
                            attributes[j].value.RemoveModifier(_slot.item.buffs[i]);
                        }
                    }
                }
                break;
            case InterfaceType.Chest:
                break;

            default:
                break;
        }
    }
    public void OnAfterSlotUpdate(InventorySlot _slot)
    {
        if (_slot.ItemObject == null)
            return;
        switch (_slot.parent.inventory.type)
        {
            case InterfaceType.Inventory:
                break;
            case InterfaceType.Equipment:
                print(string.Concat("placed ", _slot.ItemObject, " on ", _slot.parent.inventory.type, ", Allowed Items: ", string.Join(", ", _slot.AllowedItems)));
                for (int i = 0; i < _slot.item.buffs.Length; i++)
                {
                    for (int j = 0; j < attributes.Length; j++)
                    {
                        if (attributes[j].type == _slot.item.buffs[i].attributes)
                        {
                            attributes[j].value.AddModifier(_slot.item.buffs[i]);
                        }
                    }
                }
                break;
            case InterfaceType.Chest:
                break;

            default:
                break;
        }
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

    public class Attribute
    {
        [System.NonSerialized]
        public Stats parent;
        public Attributes type;
        public ModifiableInt value;

        public void SetParent(Stats _parent)
        {
            parent = _parent;
            value = new ModifiableInt(AttributeModified);
        }
        public void AttributeModified()
        {
            parent.AttributeModified(this);
        }
    }


    public void OnLevelUp()
    {
        defense += 2;
        nimbleness += 2;
        brawn += 2;
        brain += 2;
        vigor += 2;
        lv = leveling.currLevel;
        statpoint++;
    }

    #endregion
}
