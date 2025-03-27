using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float runSpeed = 8f;
    public float crouchSpeed = 3f;
    public float jumpForce = 5f;
    public float crouchHeight = 0.5f;
    public float normalHeight = 2f;
    public LayerMask groundLayer;

    private Rigidbody rb;
    private CapsuleCollider col;
    private bool isGrounded;
    private bool isCrouching;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        col = GetComponent<CapsuleCollider>();
    }

    void Update()
    {
        Move();
        Jump();
        Crouch();
    }

    void Move()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 moveDirection = transform.right * moveX + transform.forward * moveZ;

        float speed = walkSpeed;
        if (Input.GetKey(KeyCode.LeftShift) && !isCrouching)
        {
            speed = runSpeed;
        }
        else if (isCrouching)
        {
            speed = crouchSpeed;
        }

        Vector3 velocity = new Vector3(moveDirection.x * speed, rb.velocity.y, moveDirection.z * speed);
        rb.velocity = velocity;
    }

    void Jump()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, col.bounds.extents.y + 0.1f, groundLayer);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !isCrouching)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
        }
    }

    void Crouch()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            isCrouching = true;
            col.height = crouchHeight;
            transform.localScale = new Vector3(1, crouchHeight / normalHeight, 1);
        }
        else if (Input.GetKeyUp(KeyCode.C))
        {
            isCrouching = false;
            col.height = normalHeight;
            transform.localScale = Vector3.one;
        }
    }
}
