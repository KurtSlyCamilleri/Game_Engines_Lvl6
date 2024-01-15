using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    public Item item;
    public Inventory inventory;

    private void OnTriggerEnter(Collider other) {
        inventory.Add(item);
    }
}
