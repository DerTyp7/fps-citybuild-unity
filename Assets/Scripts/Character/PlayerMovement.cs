using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Force Modifier")]
    [SerializeField] private float speed = 12f;
    
    [SerializeField] private float sneakSpeedModifier = 0.7f;
    [SerializeField] private float sprintSpeedModifier = 1.4f;
    [SerializeField] private float jumpSprintSpeedModifier = 20f;
    [SerializeField] private float jumpForce = 3.5f;
    [SerializeField] private float fallMultiplier = 2.5f;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float maxWalkebleAngle = 45f;

    public bool isSprinting = false;
    public bool isSneaking = false;

    private Rigidbody rb;
    private bool isGrounded;
    private float modifiedSpeed;
    private bool isJumpSprinting = false;
    float x;
    float z;
    float angle;

    Vector3 movement;
    Vector3 moveDirection;
    Vector3 angleDir;



    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Grounded();
        Jump();
        MovementSpeed();
    }

    private void FixedUpdate()
    {  
        rb.velocity = movement;
        
    }

    private void Grounded()
    {
        //Check every frame if the player stands on the ground
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        RaycastHit hit;
        
        if (Physics.Raycast(gameObject.transform.position, Vector3.down, out hit)) {
            Vector3 t = hit.normal;
            angle = Vector3.Angle(t,Vector3.up);
            angleDir = new Vector3(t.x,0,t.z).normalized;

        }
        Vector3 g = angleDir - Vector3.forward;
        Debug.Log(g.magnitude);
        if (angle > maxWalkebleAngle && !(g.magnitude > 1))
        {
            isGrounded = false;
        }

        if (isGrounded)
        {
            isJumpSprinting = false;
        }

    }
    private void MovementSpeed()
    {
        modifiedSpeed = speed; //RESET speed


        //Sprint
        if (Input.GetButton("Sprint") && isGrounded)
        {
            isSprinting = true;
            modifiedSpeed *= sprintSpeedModifier;
        }
        else
        {
            isSprinting = false;
            //Sneak
            if (Input.GetButton("Sneak") && isGrounded)
            {
                isSneaking = true;
                modifiedSpeed *= sneakSpeedModifier;
            }
            else
            {
                isSneaking = false;
            }
        }    

        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");
        moveDirection.Set(x,0,z);
        movement = x * modifiedSpeed * transform.right + new Vector3(0, rb.velocity.y, 0) + z * modifiedSpeed * transform.forward;
    }

    private void Jump()
    {
        //Better Falling
        if(rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            //Add Force Up To Jump
            rb.AddForce(Vector3.up * jumpForce * 100); //Times 100 -> so we can use smaller numbers in the editor
        }
    }
}
