using UnityEngine;

public class Plunger : MonoBehaviour {
    private const float DetectionRadius = 5f;
    private const float MoveDistance = 7.0f;
    private const float MoveSpeed = 30.0f;
    private bool plungerMoving = false;
    private bool moved = false;

    private Rigidbody rb;

    void Start() {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }

    void Update() {
        CheckIfPlayerInRange();
    }

    void CheckIfPlayerInRange() {
        Collider[] colliders = Physics.OverlapSphere(transform.position, DetectionRadius);

        foreach (Collider collider in colliders) {
            if (collider.CompareTag("Player") && !plungerMoving && !moved) {
                StartCoroutine(MovePlunger());
                break;
            }
        }
    }

    System.Collections.IEnumerator MovePlunger() {
        plungerMoving = true;

        Vector3 initialPosition = transform.position;
        Vector3 targetPosition = initialPosition + transform.forward * MoveDistance;

        while (Vector3.Distance(transform.position, targetPosition) > 0.01f) {
            rb.MovePosition(Vector3.MoveTowards(transform.position, targetPosition, MoveSpeed * Time.deltaTime));
            yield return null;
        }

        while (Vector3.Distance(transform.position, initialPosition) > 0.01f) {
            rb.MovePosition(Vector3.MoveTowards(transform.position, initialPosition, MoveSpeed * Time.deltaTime));
            yield return null;
        }

        plungerMoving = false;
        moved = true;
    }
}
