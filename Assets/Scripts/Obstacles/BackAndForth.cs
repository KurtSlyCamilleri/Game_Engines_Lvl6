using UnityEngine;

public class BackAndForth : MonoBehaviour {
    public GameObject startPoint;
    public GameObject endPoint;
    public float speed = 5f;
    public float slowdownDistance = 2f;
    public float accelerationDistance = 2f;

    private Transform target;
    private Rigidbody rigidBody;

    private void Start() {
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.isKinematic = true;
        target = endPoint.transform;
    }

    private void Update() {
        MoveTowards(target.position);
    }

    private void MoveTowards(Vector3 targetPosition) {
        float step = CalculateMovementStep(targetPosition);

        if (step > 0.001f) {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
        } else {
            target = (target == startPoint.transform) ? endPoint.transform : startPoint.transform;
        }
    }

    private float CalculateMovementStep(Vector3 targetPosition) {
        float distanceToTarget = Vector3.Distance(transform.position, targetPosition);
        float accelerationFactor = Mathf.Clamp01(distanceToTarget / accelerationDistance);
        float slowdownFactor = Mathf.Clamp01(distanceToTarget / slowdownDistance);
        float currentSpeed = speed * Mathf.Lerp(accelerationFactor, slowdownFactor, slowdownFactor);
        return currentSpeed * Time.deltaTime;
    }
    

    private void OnTriggerEnter(Collider other) {
        GameObject otherGameObject = other.gameObject;

        if (otherGameObject == startPoint) {
            target = endPoint.transform;
        } else if (otherGameObject == endPoint) {
            target = startPoint.transform;
        }
    }
}
