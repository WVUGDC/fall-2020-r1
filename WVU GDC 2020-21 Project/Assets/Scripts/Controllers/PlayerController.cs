using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //holds the player's ID
    //public int playerId;

  
    
    [Header("Character Attributes:")]
    //base speed of character
    public float MOVEMENT_BASE_SPEED = 1.0f;

    [Space]
    [Header("Character Statistics:")]
    //speed of character
    public float movementSpeed;
    //holds the direction of the character
    public Vector2 movementDirection;

    [Space]
    [Header("References:")]
    //basic physics for the character
    public Rigidbody2D rb;
    //Animation for character
    public Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInputs();
        Move();
        Animate();
    }

    void ProcessInputs()
    {
        movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        //locks the speed between two points
        movementSpeed = Mathf.Clamp(movementDirection.magnitude, 0.0f, 1.0f);

        //makes sure movementDirection is a unit vector
        movementDirection.Normalize();
    }

    void Move()
    {
        //sets the velocity of the rigidbody based on the movement direction, movement speed, and base speed
        rb.velocity = movementDirection * movementSpeed * MOVEMENT_BASE_SPEED;
    }

    void Animate()
    {
        //Modifies the animator's Horizontal and Vertical floats based on the direction of movement
        animator.SetFloat("Horizontal", movementDirection.x);
        animator.SetFloat("Vertical", movementDirection.y);

        //Modifies the animator's Speed float based on movementSpeed
        animator.SetFloat("Speed", movementSpeed);
    }
}
