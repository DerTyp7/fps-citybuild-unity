using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Speed Modifier")]
    [SerializeField] private float speed = 12f;
    [SerializeField] private float adjustedSpeed;
    [SerializeField] private float sneakSpeed = 0.4f;
    [SerializeField] private float sprintSpeed = 1.8f;
    [SerializeField] private float jumpHeight = 3f;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;


    private Rigidbody rb;
    private bool isGrounded;
    private bool isSprinting = false;
    private bool isSneaking = false;
    Vector3 movement;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //Check every frame if the player stands on the ground
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        
        //JUMP
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            //Add Force Up To Jump
            rb.AddForce(Vector3.up * jumpHeight * 100); //Times 100 -> so we can use smaller numbers in the editor
        }


        //Sprint
        if (Input.GetButton("Sprint") && isGrounded)
        {
            Debug.Log("[PlayerController] Sprinting");
            isSprinting = true;
            adjustedSpeed *= sprintSpeed;
        }
        else
        {
            isSprinting = false;
            //Sneak
            if (Input.GetButton("Sneak") && isGrounded)
            {
                Debug.Log("[PlayerController] Sneaking");
                isSprinting = true;
                adjustedSpeed *= sneakSpeed;
            }
            else
            {
                isSneaking = false;
            }
        }        
    }

    private void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");


        movement = x * adjustedSpeed * transform.right + new Vector3(0, rb.velocity.y, 0) + z * adjustedSpeed * transform.forward;
        rb.velocity = movement;
        adjustedSpeed = speed;
    }

}
