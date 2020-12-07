using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public enum Itemtype
    {
        Consumables,
        Equipment,
        Default
    }

    public abstract class ItemObject : ScriptableObject
    {
        public GameObject prefab;
        public Itemtype itemtype;
        [TextArea(15, 20)]
        public string description;

    }

