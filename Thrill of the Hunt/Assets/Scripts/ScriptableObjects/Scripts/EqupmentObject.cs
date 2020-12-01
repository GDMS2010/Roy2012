using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment Object", menuName = "Inventory System/Items/Equipment")]
public class EqupmentObject : ItemObject
{
    public float AtkBonus;
    public float DefBonus;
    public void Awake()
    {
        itemtype = Itemtype.Equipment;
    }
}
