using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickBehaviour : MonoBehaviour {
    [SerializeField] EnablePicking enablePicking;
    [SerializeField] PinBehaviour pinBehaviour; // Add reference to PinBehaviour script

    public float moveSpeed = 15f;
    private Vector3 originalPosition;

    private void Start() {
        // Store the original position at the start
        originalPosition = transform.position;
    }

    private void Update() {
        if (enablePicking.isPicking == true) {
            MovePick();
        }
    }

    private void MovePick() {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        // Check if any pin is pinned
        if (pinBehaviour.IsAnyPinned()) {
            // If pinned, only allow vertical movement
            horizontalInput = 0f;
        }

        Vector2 moveDirection = new Vector2(horizontalInput, verticalInput).normalized;
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime * 100);
    }

    public void ResetPickPosition() {
        transform.position = originalPosition;
    }
}
