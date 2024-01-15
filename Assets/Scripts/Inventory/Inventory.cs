using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "custom/inventory")]
public class Inventory : ScriptableObject
{
    public List<Item> items = new List<Item>();

    public void Add(Item newItem) {
        items.Add(newItem);
    }
}
