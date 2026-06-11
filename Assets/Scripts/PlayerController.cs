using System;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    [SerializeField] private float rotationSpeed = 15f;
    [SerializeField] private float health = 100f;
    
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletSpawn;

    private Rigidbody rb;
    private Vector2 inputVector;
    private bool isGrounded;
    private bool jumpRequested;
    private Camera mainCamera;

    
    
    
    
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        mainCamera = Camera.main;
        
        if (mainCamera == null)
            Debug.LogError("На сцене не найдена камера с тегом MainCamera!");
    }
    
    private void Update()
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
            Instantiate(bulletPrefab, bulletSpawn.position, transform.rotation);
        }
        
        RotateTowardsMouse();
    }

    private void FixedUpdate()
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






    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
    
    private void RotateTowardsMouse()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane virtualGroundPlane = new Plane(Vector3.up, transform.position);
       
        float rayDistance;
        if (virtualGroundPlane.Raycast(ray, out rayDistance))
        {
            Vector3 targetPosition = ray.GetPoint(rayDistance);
            Vector3 direction = targetPosition - transform.position;
            direction.y = 0f;

            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);

                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
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