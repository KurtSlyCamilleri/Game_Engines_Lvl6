using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour {

    [SerializeField] private GameObject OpenedSuitcase;
    [SerializeField] private GameObject ClosedSuitcase;
    [SerializeField] private GameObject InventorySlider;
    private bool invEnabled = false;

    void Update() {
        if (Input.GetKeyDown(KeyCode.Tab)) {
            // Toggle the inventory state
            invEnabled = !invEnabled;

            // Call the appropriate method based on the inventory state
            if (invEnabled) {
                OpenInventory();
            } else {
                CloseInventory();
            }
        }
    }

    public void OpenInventory() {
        OpenedSuitcase.SetActive(true);
        ClosedSuitcase.SetActive(false);
        InventorySlider.SetActive(true);
    }

    public void CloseInventory() {
        ClosedSuitcase.SetActive(true);
        OpenedSuitcase.SetActive(false);
        InventorySlider.SetActive(false);
    }
}
