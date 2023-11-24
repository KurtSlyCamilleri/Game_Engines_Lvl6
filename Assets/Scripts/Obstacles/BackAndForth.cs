using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackAndForth : MonoBehaviour {
    public Collider BackCollider;
    public Collider FrontCollider;
    private Collider colliderOpt;
    public float moveSpeed = 10f;

    private bool isMovingForward = true;
    private Rigidbody rb;

    private void Awake() {
        colliderOpt = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }
    private void Update() {
        if (IsCollidingWith(BackCollider)) {
            isMovingForward = true;
        }
        if (IsCollidingWith(FrontCollider)) {
            isMovingForward = false;
        }
        Vector3 movement = isMovingForward ? Vector3.forward : Vector3.back;
        transform.position += movement * moveSpeed * Time.deltaTime;
    }
    private bool IsCollidingWith(Collider collider) {
        return collider.bounds.Intersects(colliderOpt.bounds);
    }
}