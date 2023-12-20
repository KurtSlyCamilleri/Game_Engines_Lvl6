using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public class MoveWithPlatform : MonoBehaviour{
    
    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Player")) {
            Rigidbody playerRigidbody = other.gameObject.GetComponent<Rigidbody>();
            playerRigidbody.interpolation = RigidbodyInterpolation.None;
            playerRigidbody.collisionDetectionMode = CollisionDetectionMode.Discrete;

            other.transform.SetParent(transform);
        }
    }
    private void OnCollisionExit(Collision other) {
        if (other.gameObject.CompareTag("Player")) {
            Rigidbody playerRigidbody = other.gameObject.GetComponent<Rigidbody>();
            playerRigidbody.interpolation = RigidbodyInterpolation.None;
            playerRigidbody.collisionDetectionMode = CollisionDetectionMode.Discrete;

            other.transform.SetParent(null);

        }
    }
}
