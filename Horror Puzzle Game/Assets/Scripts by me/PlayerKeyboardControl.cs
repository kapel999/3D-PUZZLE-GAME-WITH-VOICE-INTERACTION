using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKeyboardControl : MonoBehaviour
{
    // Variable that will allow for a link to the character controller component
    public CharacterController characterController;
    // Variable that will allow to change the speed at which character moves
    public float speed = 12f;
    // Variable that will store the gravity
    public float gravity = -9.81f;
    // Variable that will store how high the character in the game will be able to jump
    public float jumpHeight = 3f;

    // Variable that will allow for link between this class and the "Ground Check" object
    public Transform groundCheck;
    // Variable that will specify the radius of the "Ground Check" object
    public float groundDistance = 0.4f;
    // Variable that will allow to control what objects the "Ground Check" object should check for
    public LayerMask groundMask;

    // Variable that will store the velocity of the character
    private Vector3 velocity;
    // Variable that will check if the "Ground Check" is touching an object or not
    private bool isTouchingGround;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // This line of code will check if the sphear "GroundCheck" objeck is trouching an object
        // and if so return true else false
        isTouchingGround = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // If statement that will check if player is all ready touching the ground and if
        // the velocity of y is above 0.
        if(isTouchingGround && velocity.y < 0)
        {
            // If the checks passes the velocity will be reset to -3 as it seems to work best in my game even
            // better than zero as it forces player on the ground
            velocity.y = -3f;
        }

        // This will store the input as float variables
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Stores as variable where the player wants to go based on the keyboard input
        Vector3 move = transform.right * x + transform.forward * z;

        // Uses the "Move" functionality from the character controller to move the character based on the
        // move variable that is multiplied by speed so the character can move at different speeds and 
        // "Time.deltaTime" as I am working inside of the udpdate method.
        characterController.Move(move * speed * Time.deltaTime);

        // If statment that will check if the jump button is pressed and if the character is tuching the ground
        if(Input.GetButtonDown("Jump") && isTouchingGround)
        {
            // If the check passes then velocity will be calcualted and set to a player so player will jump
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Velocity will be calculated by adding to it gravity that will be times by time
        velocity.y += gravity * Time.deltaTime;

        // This will allow the player to fall down when player isn't standing on anything
        characterController.Move(velocity * Time.deltaTime);
    }
}
