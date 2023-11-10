using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackAndForth : MonoBehaviour
{
    
    
    public Collider BackCollider;
    public Collider FrontCollider;
    private Collider colliderOpt;
    public float moveSpeed = 5f;

    private bool isMovingForward = true; // Initial direction

    private void Awake() {
        colliderOpt = GetComponent<Collider>();
    }

    void Update() {
        if (IsCollidingWith(BackCollider)) {// Check if the object is colliding with the left collider
            isMovingForward = true; // Collided with left, so move right
        }
        if (IsCollidingWith(FrontCollider)) {// Check if the object is colliding with the right collider
            isMovingForward = false; // Collided with right, so move left
        }
        if (isMovingForward) {// Move the object based on the current direction
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        } else {
            transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
        }
    }
    
    private bool IsCollidingWith(Collider collider) {// Function to check if the object is colliding with a specific collider
        return collider.bounds.Intersects(colliderOpt.bounds);
    }
    

}
