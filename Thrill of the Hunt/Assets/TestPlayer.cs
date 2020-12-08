using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    public InventoryObject inventory;

    public float speed = 50;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

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

        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Forward");

        Vector3 movement = new Vector3(hAxis, 0, vAxis) * speed * Time.deltaTime;

        rb.MovePosition(transform.position + movement);
    }
    private void OnApplicationQuit()
    {
        inventory.Container.Items = new InventorySlot[36];
    }

 
}
