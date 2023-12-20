using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slip : MonoBehaviour
{
    public float slipSpeed = 5f;
    private void OnTriggerStay(Collider other) {
        if (other.CompareTag("Player")) {
            Vector3 playerDirection = other.GetComponent<Rigidbody>().velocity.normalized;
            playerDirection.y = 0;
            Vector3 slipMovement = playerDirection * slipSpeed * Time.deltaTime;
            other.transform.position += slipMovement;
        }
    }
}
