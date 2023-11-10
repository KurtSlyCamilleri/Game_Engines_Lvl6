using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plunger : MonoBehaviour {
    [System.NonSerialized] private float detectionRange = 1.5f;
    [System.NonSerialized] private float coneRadius = 5f; // Adjust the cone radius as needed
    [System.NonSerialized] private bool plungerMoving = false;
    [System.NonSerialized] private float moveDistance = 7.0f; // Adjust the distance to move the plunger

    void Update() {
        CheckIfPlayerInRange();
    }

    void CheckIfPlayerInRange() {
        RaycastHit hit;

        bool sphereCastHit = Physics.SphereCast(transform.position, coneRadius, transform.forward, out hit, detectionRange);
        bool playerInRange = sphereCastHit && hit.collider.CompareTag("Player");

        if (playerInRange) {
            if (!plungerMoving) {
                StartCoroutine(MovePlunger());
            }
        }
    }

    IEnumerator MovePlunger() {
        plungerMoving = true;

        Vector3 initialPosition = transform.position;
        Vector3 targetPosition = transform.position + transform.forward * moveDistance;

        float startTime = Time.time;
        float journeyLength = Vector3.Distance(initialPosition, targetPosition);
        float duration = 0.5f; // Adjust the duration as needed

        while (Time.time - startTime < duration) {
            float fractionOfJourney = (Time.time - startTime) / duration;
            transform.position = Vector3.Lerp(initialPosition, targetPosition, fractionOfJourney);
            yield return null;
        }

        // Plunger reached the target position, now move it back
        startTime = Time.time;
        while (Time.time - startTime < duration) {
            float fractionOfJourney = (Time.time - startTime) / duration;
            transform.position = Vector3.Lerp(targetPosition, initialPosition, fractionOfJourney);
            yield return null;
        }

        plungerMoving = false;
    }
}
