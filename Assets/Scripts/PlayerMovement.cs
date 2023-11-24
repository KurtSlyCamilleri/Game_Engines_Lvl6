using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerMovement : MonoBehaviour {
    public float baseMoveSpeed = 5f;
    [System.NonSerialized] public float jumpForce = 7f;//don't touch nonserialized
    public bool isGrounded;
    private bool isOnLadder;
    [System.NonSerialized]public float turnSpeed = 15f;
    public Rigidbody rb;
    public float raycastDistance = 0.1f;
    public LayerMask groundLayer;
    public LayerMask ladderLayer;
    public float climbSpeed = 5f;
    public bool isParalyzed = false;
    public bool RMBToggle = false;

    void Start() {
        RigidBody();
    }
    
    void Update() {
        if (isParalyzed == false) {
            MovementInput();
            TurnPlayer(); 
        }
        Jump();
        JumpOffLadder();
        CheckGround();
        CheckLadder();
        Climbing();
    }
    
    void RigidBody() { //anything that has to do with rigidbody is to be put here
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // Prevents the player from flipping over
    }
    
    void MovementInput() {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;
        MovePlayer(moveDirection);
    }
    
    void MovePlayer(Vector3 moveDirection) {
        
        Vector3 targetVelocity = transform.TransformDirection(moveDirection) * baseMoveSpeed;
        rb.velocity = new Vector3(targetVelocity.x, rb.velocity.y, targetVelocity.z);
    }
    
    void TurnPlayer() {
        //separate the rmb toggles into separate methods for the sake of other scripts
        if (Input.GetMouseButtonDown(1)) {
            RMBToggle = !RMBToggle; // Toggle the state
        }

        if (RMBToggle) {
            float mouseX = Input.GetAxis("Mouse X");
            Vector3 rotation = new Vector3(0f, mouseX * turnSpeed, 0f);
            Quaternion deltaRotation = Quaternion.Euler(rotation * Time.fixedDeltaTime);
            rb.MoveRotation(rb.rotation * deltaRotation);

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        } else {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
        
    }
    
    void CheckGround() {//raycast downwards to check for the ground
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, raycastDistance, groundLayer)) {
            isGrounded = true;
   
        } else {
            isGrounded = false;
        }
    }

    //parkour movements

    void Jump() {
        if (Input.GetButtonDown("Jump") && isGrounded) {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

    }
     
    void JumpOffLadder() {
        if (isOnLadder && Input.GetButtonDown("Jump")) {
            Vector3 jumpDirection = -transform.forward + Vector3.up;// Calculate backward jump direction
            rb.AddForce(jumpDirection * jumpForce, ForceMode.Impulse);
            isOnLadder = false;
        }

    }
    
    void CheckLadder() {//raycast forwards to check for ladder
        RaycastHit hit;
        Vector3 raycastDirection = transform.forward;
        if (Physics.Raycast(transform.position, raycastDirection, out hit, raycastDistance, ladderLayer)) {
            isOnLadder = true;
        } else {
            isOnLadder = false;
        }
    }
    
    void Climbing() {
        if (isOnLadder && !isGrounded) {
            float climbInput = Input.GetAxis("Vertical");
            float horizontalClimbInput = Input.GetAxis("Horizontal");
            Vector3 climbDirection = new Vector3(0f, climbInput * climbSpeed, 0f);// Vertical climbing movement
            rb.velocity = new Vector3(rb.velocity.x, climbDirection.y, rb.velocity.z);
            Vector3 horizontalMovement = new Vector3(horizontalClimbInput * baseMoveSpeed, 0f, 0f);// Horizontal climbing movement
            rb.velocity += transform.TransformDirection(horizontalMovement) * Time.deltaTime;
        }
    }
}