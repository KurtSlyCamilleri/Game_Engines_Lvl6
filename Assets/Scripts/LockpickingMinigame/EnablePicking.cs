using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnablePicking : MonoBehaviour
{
    [SerializeField]private GameObject lockpicking;
    public bool isPicking = false;

    [SerializeField]private PinBehaviour pinBehaviour;
    //if player clicks on object the lockpicking box is enabled 
    void Update() {
        // Check for left mouse button click
        if (Input.GetMouseButtonDown(0)) {
            // Cast a ray from the mouse position into the scene
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Check if the ray hits a collider
            if (Physics.Raycast(ray, out hit)) {
                // Check if the collider belongs to a 3D object
                if (hit.collider != null && hit.collider.gameObject == gameObject) {
                    // Log a message to the console
                    lockpicking.SetActive(true);
                    isPicking = true;
                }
            }
        }
        if(pinBehaviour.finished == true) {
             this.gameObject.SetActive(false);
        }
        
    }
}
