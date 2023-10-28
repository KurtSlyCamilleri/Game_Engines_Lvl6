using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public float moveSpeed = 5f;
    public GameObject groundCheck;
    public string targetLayerName = "Ground";
    public float jumpForce = 2f;
    private bool isGrounded;

    private Rigidbody rb;

    //in future iterations, make the controls uniform across different perspectives(maybe make it turn, not yet concretely decided)

    void Start() {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // Prevents the player from flipping over
    }

    void Update() {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;
        MovePlayer(moveDirection);

        if (Input.GetButtonDown("Jump") && isGrounded) {
            Jump();
        }
    }

    void MovePlayer(Vector3 moveDirection) {
        Vector3 movement = moveDirection * moveSpeed;
        rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);
    }

    void Jump() {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.layer == LayerMask.NameToLayer(targetLayerName)) {
            isGrounded = true;
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.gameObject.layer == LayerMask.NameToLayer(targetLayerName)) {
            isGrounded = false;
        }
    }
}
