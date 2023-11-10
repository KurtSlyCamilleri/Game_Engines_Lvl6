using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slip : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public Transform player; // Assign the player transform in the Inspector
    public float slipSpeed = 5f; // Adjust the slipping speed
    private bool isPlayerOnObstacle = false;


    void Update() {
        if (isPlayerOnObstacle && player != null) {
            SlipPlayer();
           
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            isPlayerOnObstacle = true;
            playerMovement.isParalyzed = true;
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) {
            isPlayerOnObstacle = false;
            playerMovement.isParalyzed = false;
        }
    }

    void SlipPlayer() {
        // Move the player forward based on the slipping speed
        player.Translate(Vector3.forward * slipSpeed * Time.deltaTime);
    }
}
