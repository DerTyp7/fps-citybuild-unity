using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private CharacterController controller;
    //[SerializeField] private Transform playerTransform;
    [SerializeField] private float speed = 12f;
    [SerializeField] private float airSpeed = 0.6f;
    [SerializeField] private float sprintSpeed = 1.8f;
    [SerializeField] private float sprintAirSpeed = 1.2f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpHeight = 3f;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;

    private Vector3 velocity;
    private bool isGrounded;
    private bool isSprinting;

    void Start()
    {
        
    }


    void Update()
    {
        //GROUND CHECK
        //Creates an invisible sphere on the bottom of our player
        //And checks if it's colliding with something !with the "ground"-Mask in Unity!
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        //Reset velocity if grounded
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
       
           
        //MOVEMENT
        //Input.GetAxis is based on the Unity Input settings (edit -> Project Settings -> Input Manager)
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        //Create move vector !in look direction!
        Vector3 move;

        if (isGrounded)//for air control
        {
             move = transform.right * x + transform.forward * z;

            //SPRINT
            if (Input.GetButton("Sprint"))
            {
                move *= sprintSpeed;
                isSprinting = true;
            }
            else
            {
                //SNEAK
                if (Input.GetButton("Sneak"))
                {
                    //Kommt mit character model und animations
                }
                isSprinting = false;
            }

            //JUMP
            if (Input.GetButtonDown("Jump"))
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }            
        }
        else//Air control
        {
            if (isSprinting)
            {
                move = transform.right * x * airSpeed * sprintAirSpeed + transform.forward * z * airSpeed * sprintAirSpeed;
            }
            else
            {
                move = transform.right * x * airSpeed + transform.forward * z * airSpeed;
            }
        }

        //Apply move vector
        controller.Move(move * speed * Time.deltaTime);

        //Add gravity to current velocity
        velocity.y += gravity * Time.deltaTime;
        //apply gravity
        controller.Move(velocity * Time.deltaTime);//nochmal time.deltatime wegen irgendwas mit physikalischer Formel und so

    }
}
