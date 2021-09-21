using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private CharacterController controller;
    [SerializeField] private float speed = 12f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpHeight = 3f;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;

    private Vector3 velocity;
    private bool isGrounded;

    void Start()
    {
        
    }


    void Update()
    { 
        //Creates an invisible sphere on the bottom of our player
        //And checks if it's colliding with something !with the "ground"-Mask in Unity!
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        //Reset velocity if grounded
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        //Input.GetAxis is based on the Unity Input settings (edit -> Project Settings -> Input Manager)
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //Create move vector !in look direction!
        Vector3 move = transform.right * x + transform.forward * z;
        //Apply move vector
        controller.Move(move * speed * Time.deltaTime);

        //JUMP
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        //Add gravity to current velocity
        velocity.y += gravity * Time.deltaTime;
        //apply gravity
        controller.Move(velocity * Time.deltaTime);//nochmal time.deltatime wegen irgendwas mit physikalischer Formel und so
    }

    void Jump()
    {

    }
}
