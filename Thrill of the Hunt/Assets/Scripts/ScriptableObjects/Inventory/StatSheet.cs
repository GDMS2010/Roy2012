using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StatSheet : MonoBehaviour
{
    public TempInfo[] charaters;
    public int index;
    public GameObject Name;
    public GameObject Level;
    public GameObject StatPoints;
    public GameObject Defense;
    public GameObject Nimbleness;
    public GameObject Brawn;
    public GameObject Brain;
    public GameObject Vigor;

    [System.Serializable]
    public class TempInfo
    {
        public string characterName;

        public InventoryObject inventory;
        public InventoryObject equipment;

        public int lv;
        public Leveling leveling;
        public int statpoint;
        public Attribute[] attributes;

        [Header("Attributes")]
        public int defense;
        public int nimbleness;
        public int brawn;
        public int brain;
        public int vigor;
        public int initiative;

        public void AttributeModified(Attribute attribute)
        {
            Debug.Log(string.Concat(attribute.type, "was updated! Value is now ", attribute.value.ModifiedValue));
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        index = 0;

        for (int i = 0; i < charaters[index].equipment.GetSlots.Length; i++)
        {
            charaters[index].equipment.GetSlots[i].OnBeforeUpdate += OnBeforeSlotUpdate;
            charaters[index].equipment.GetSlots[i].OnAfterUpdate += OnAfterSlotUpdate;
        }

        for (int i = 0; i < charaters.Length; i++)
        {
            charaters[index].leveling = new Leveling(1, OnLevelUp);
        }

        UpdateUI();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            charaters[index].leveling.AddExp(100) ;
            UpdateUI();
        }
    }

    public void CycleForward()
    {
        if (index >= charaters.Length - 1)
        {
            index = 0;
        }
        else
        {
            index++;
        }

        UpdateUI();
    }

    public void CycleBackwards()
    {
        if (index == 0)
        {
            index = 3;
        }
        else
        {
            index--;
        }

        UpdateUI();
    }

    public void IncreaseDefense()
    {
        if (charaters[index].statpoint >= 1)
        {
            charaters[index].defense++;
            charaters[index].statpoint--;
            UpdateUI();
        }
    }

    public void IncreaseNimbleness()
    {
        if (charaters[index].statpoint >= 1)
        {
            charaters[index].nimbleness++;
            charaters[index].statpoint--;
            UpdateUI();
        }
    }

    public void IncreaseBrawn()
    {
        if (charaters[index].statpoint >= 1)
        {
            charaters[index].brawn++;
            charaters[index].statpoint--;
            UpdateUI();
        }
    }

    public void IncreaseBrain()
    {
        if (charaters[index].statpoint >= 1)
        {
            charaters[index].brain++;
            charaters[index].statpoint--;
            UpdateUI();
        }
    }

    public void IncreaseVigor()
    {
        if (charaters[index].statpoint >= 1)
        {
            charaters[index].vigor++;
            charaters[index].statpoint--;
            UpdateUI();
        }
    }
    public void UpdateUI()
    {
        Name.GetComponent<Text>().text = charaters[index].characterName.ToString();
        StatPoints.GetComponent<TextMeshProUGUI>().text = charaters[index].statpoint.ToString();
        Level.GetComponent<TextMeshProUGUI>().text = charaters[index].lv.ToString();
        Defense.GetComponent<TextMeshProUGUI>().text = charaters[index].defense.ToString();
        Nimbleness.GetComponent<TextMeshProUGUI>().text = charaters[index].nimbleness.ToString();
        Brawn.GetComponent<TextMeshProUGUI>().text = charaters[index].brawn.ToString();
        Brain.GetComponent<TextMeshProUGUI>().text = charaters[index].brain.ToString();
        Vigor.GetComponent<TextMeshProUGUI>().text = charaters[index].vigor.ToString();
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
                    for (int j = 0; j < charaters[index].attributes.Length; j++)
                    {
                        if (charaters[index].attributes[j].type == _slot.item.buffs[i].attributes)
                        {
                            charaters[index].attributes[j].value.RemoveModifier(_slot.item.buffs[i]);
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
                    for (int j = 0; j < charaters[index].attributes.Length; j++)
                    {
                        if (charaters[index].attributes[j].type == _slot.item.buffs[i].attributes)
                        {
                            charaters[index].attributes[j].value.AddModifier(_slot.item.buffs[i]);
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

    public class Attribute
    {
        [System.NonSerialized]
        public TempInfo parent;
        public Attributes type;
        public ModifiableInt value;

        public void SetParent(TempInfo _parent)
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
        charaters[index].defense += 2;
        charaters[index].nimbleness += 2;
        charaters[index].brawn += 2;
        charaters[index].brain += 2;
        charaters[index].vigor += 2;
        charaters[index].lv = charaters[index].leveling.currLevel;
        charaters[index].statpoint++;
    }
}
