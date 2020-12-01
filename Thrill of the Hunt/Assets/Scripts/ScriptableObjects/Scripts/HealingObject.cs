using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Healing Object", menuName = "Inventory System/Items/Health")]
public class HealingObject : ItemObject
{
    public int restoreHealthValue;
    public void Awake()
    {
        itemtype = Itemtype.Consumables;      
    }
    
}
