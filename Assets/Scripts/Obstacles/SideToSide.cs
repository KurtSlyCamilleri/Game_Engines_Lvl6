using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideToSide : MonoBehaviour
{
    public Collider leftCollider;
    public Collider rightCollider;
    private Collider colliderOpt;
    public float moveSpeed = 5f;

    private bool isMovingRight = true; // Initial direction

    private void Awake() {
        colliderOpt = GetComponent<Collider>();
    }

    void Update() {
        
        if (IsCollidingWith(leftCollider)) { // Check if the object is colliding with the left collider
            isMovingRight = true; // Collided with left, so move right
        }
        if (IsCollidingWith(rightCollider)) { // Check if the object is colliding with the right collider
            isMovingRight = false; // Collided with right, so move left
        }
        if (isMovingRight) { // Move the object based on the current direction
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        } else {
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        }
    }

    // Function to check if the object is colliding with a specific collider
    private bool IsCollidingWith(Collider collider) {
        return collider.bounds.Intersects(colliderOpt.bounds);
    }
}
