using UnityEngine;

public class BackAndForth : MonoBehaviour {
    public GameObject startPoint;
    public GameObject endPoint;
    public float speed = 5f;

    private bool movingTowardsEnd = true;
    private Rigidbody rigidBody;

    private void Start() {
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.isKinematic = true; // Set rigidbody to kinematic
    }

    private void Update() {
        Vector3 targetPosition = movingTowardsEnd ? endPoint.transform.position : startPoint.transform.position;
        targetPosition.y = transform.position.y; // Ignore up and down movement

        MoveTowards(targetPosition);
    }

    private void MoveTowards(Vector3 targetPosition) {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject == startPoint) {
            movingTowardsEnd = true;
        } else if (other.gameObject == endPoint) {
            movingTowardsEnd = false;
        }
    }
}
