using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerB : MonoBehaviour
{

public Transform groundCheckPoint;
public float groundCheckDistance = 0.1f;
public LayerMask GroundLayer;
private bool isGrounded;

private Animator myAnimator;
private Rigidbody2D myRigidbody;

private bool isMoving;
private bool isRunning;
private bool landing;


public float walkSpeed = 1f;
public float runSpeed = 3f;
public float speed = 1f;
public float jumpForce = 5f;


private float moveDirection;
private float maxSpeed;





void Start()
{
    myAnimator = gameObject.GetComponent<Animator>();
    myRigidbody = gameObject.GetComponent<Rigidbody2D>();
}

void Update()
{
    
    
    // Input Jump
    
    if (isGrounded)                           
    {                                         
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ApplyJump();
        }
    }                                   



    // Input Movement
    moveDirection = Input.GetAxis("Horizontal");
    if (moveDirection != 0 && isGrounded) 
    {
        isMoving = true;
        myAnimator.SetBool("Move",isMoving);  
    }
    else
    {
        isMoving = false;
        myAnimator.SetBool("Move",isMoving);
    }

    
    
    // Input Run
    if (Input.GetKey(KeyCode.LeftShift))
    {
        isRunning = true;
        myAnimator.SetBool("Run",isRunning); 
    }
    else
    {
        isRunning = false;
        myAnimator.SetBool("Run",isRunning);
    }

    
    
    // Input Special Move
    if (Input.GetKeyDown(KeyCode.E))
    {
        ApplySpecialMove();
    }
    
    
    LeftRight();                                                    // this Switches your Character between looking Left or Right
}

private void FixedUpdate()
{
     GrounCheck();                                              
    if (isMoving && !isRunning)
    {
        maxSpeed = walkSpeed;
            myRigidbody.MovePosition(gameObject.transform.position + new Vector3(moveDirection * speed * maxSpeed * Time.deltaTime,0,0));
    }
    if (isMoving && isRunning)
    {
        maxSpeed = runSpeed;
        myRigidbody.MovePosition(gameObject.transform.position + new Vector3(moveDirection * speed * maxSpeed  * Time.deltaTime,0,0));
    }

    /*if (landing)
    {
        if (isGrounded)
        {
            myAnimator.SetTrigger("Land");                                          // Triggers Animation for "Land" > Change the name to match yours
        }
    }*/



}

// This is Called when Jumping
private void ApplyJump()
{
    myAnimator.SetTrigger("Jump");                                                   // Triggers Animation for "Jump" > Change the name to match yours
    myRigidbody.AddForce(new Vector2(0,jumpForce), ForceMode2D.Impulse);
    landing = true;
}

// This is Called when the special Move is Called
private void ApplySpecialMove()
{
    myAnimator.SetTrigger("SpecialMove");                                            // Triggers Animation for the "SpecialMove" > Change the name to match yours
}


// This is Called to Check if your Character is looking left or right
private void LeftRight()
{
    if (moveDirection > 0)
    {
        gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
    }
    else if (moveDirection < 0)
    {
        gameObject.transform.eulerAngles = new Vector3(0, 180, 0);
    }
}



// This is here to check if your Player is Grounded


private void GrounCheck()
{
    isGrounded =
        Physics2D.Raycast(new Vector2(groundCheckPoint.transform.position.x, groundCheckPoint.transform.position.y),
            Vector2.down, groundCheckDistance, GroundLayer);
    myAnimator.SetBool("Ground",isGrounded);                                            // Boolean Animation for the "Grounded" > Change the name to match yours

}

}
