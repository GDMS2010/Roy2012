using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    public InventoryObject inventory;

    public MouseItem mouseItem = new MouseItem();

    public void OnTriggerEnter(Collider other)
    {
        var item = other.GetComponent<WorldItem>();
        if (item)
        {
            inventory.AddItem(new Item(item.item), 1);
            Destroy(other.gameObject);
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            inventory.Save();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            inventory.Load();
        }
    }
    private void OnApplicationQuit()
    {
        inventory.Container.Items = new InventorySlot[36];
    }
}
