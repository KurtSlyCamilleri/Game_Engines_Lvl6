using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideToSide : MonoBehaviour {
    public GameObject startPoint;
    public GameObject endPoint;
    public float speed = 5f;

    private bool movingRight = true;

    private void Update() {
        Vector3 targetPosition = movingRight ? endPoint.transform.position : startPoint.transform.position;
        targetPosition.y = transform.position.y; // Ignore up and down movement

        MoveTowards(targetPosition);
    }

    private void MoveTowards(Vector3 targetPosition) {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

        // Check if the object reached the current target position
        if (Vector3.Distance(transform.position, targetPosition) < 0.001f) {
            // If at the start point, start moving to the right; if at the end point, start moving to the left
            movingRight = !movingRight;
        }
    }

}