using System;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletSpawn;

    private Rigidbody rb;
    private Vector2 inputVector;
    private bool isGrounded;
    private bool jumpRequested;

    
    
    
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        inputVector = new Vector2(moveX, moveY).normalized;

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            jumpRequested = true;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
        }
    }

    void FixedUpdate()
    {
        Vector3 moveDirection = transform.forward * inputVector.y + transform.right * inputVector.x;
        Vector3 targetVelocity = moveDirection * moveSpeed;
        targetVelocity.y = rb.linearVelocity.y;
        
        rb.linearVelocity = targetVelocity;

        if (jumpRequested)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpRequested = false;
        }
    }

    
    
    private void OnCollisionEnter(Collision collision)
    {
        isGrounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }
}