using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniversalDamage : MonoBehaviour
{

    public int HealthReduction = 1;
    public GameManager gameManager;
    private float contactTime = 0f;
    private bool isPlayerInContact = false;

    // Update is called once per frame
    void Update() {
        RepeatOnProlongedContact();
    }

    private void RepeatOnProlongedContact() {
        if (isPlayerInContact) {
            contactTime += Time.deltaTime;

            if (contactTime >= 1f) {
                gameManager.playerHealth -= HealthReduction;
                contactTime = 0f;
            }
        }
    }



    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            isPlayerInContact = true;
            gameManager.playerHealth -= HealthReduction; // Immediate damage upon entering
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) {
            isPlayerInContact = false;
            contactTime = 0f;
        }
    }
}
