using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Inventory inventory;

    public Image prefab;

    private List<Image> images = new List<Image>();

    private void OnEnable() {
        foreach (Image child in images) {
            Destroy(child.gameObject);
        }
        images.Clear();

        // Create as many items in the visible inventory as there are in the inventory itself
        foreach (Item item in inventory.items) {
            Image img = Instantiate(prefab, transform); // Instantiate the item image accordingly
            img.sprite = item.sprite; // Change the sprite of the image to the sprite of the item that populates the inventory slot 
            images.Add(img);
        }

    }
}
