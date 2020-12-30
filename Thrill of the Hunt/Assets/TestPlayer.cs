using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TestPlayer : MonoBehaviour
{
    public InventoryObject inventory;
    public InventoryObject equipment;
    public Leveling level;

    public float speed = 50;
    private Rigidbody rb;

    public Attribute[] attributes;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        level = new Leveling(1, OnLevelUp);

        for (int i = 0; i < attributes.Length; i++)
        {
            attributes[i].SetParent(this);
        }
        for (int i = 0; i < equipment.GetSlots.Length; i++)
        {
            equipment.GetSlots[i].OnBeforeUpdate += OnBeforeSlotUpdate;
            equipment.GetSlots[i].OnAfterUpdate += OnAfterSlotUpdate;
        }
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
                        if(attributes[j].type == _slot.item.buffs[i].attributes)
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

    public void OnLevelUp()
    {
        print("level up");
    }

    public void OnTriggerEnter(Collider other)
    {
        var item = other.GetComponent<WorldItem>();
        if (item)
        {
            Item _item = new Item(item.item);
            if(inventory.AddItem(_item, 1))
            {
            Destroy(other.gameObject);
            }
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            inventory.Save();
            equipment.Save();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            inventory.Load();
            equipment.Load();
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            level.AddExp(level.GetXPForLevel(level.currLevel));
        }

        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Forward");

        Vector3 movement = new Vector3(hAxis, 0, vAxis) * speed * Time.deltaTime;

        rb.MovePosition(transform.position + movement);
    }

    public void AttributeModified(Attribute attribute)
    {
        Debug.Log(string.Concat(attribute.type, "was updated! Value is now ", attribute.value.ModifiedValue));
    }

    private void OnApplicationQuit()
    {
        inventory.Clear();
        inventory.Clear();
    }

 
}

[System.Serializable]
public class Attribute
{
    [System.NonSerialized]
    public TestPlayer parent;
    public Attributes type;
    public ModifiableInt value;

    public void SetParent(TestPlayer _parent)
    {
        parent = _parent;
        value = new ModifiableInt(AttributeModified);
    }
    public void AttributeModified()
    {
        parent.AttributeModified(this);
    }
}
