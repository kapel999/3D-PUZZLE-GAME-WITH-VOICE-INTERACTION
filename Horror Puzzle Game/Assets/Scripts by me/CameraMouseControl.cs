using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMouseControl : MonoBehaviour
{
    // Variable that will store the predefined mouse sensitivity
    public float mouseSensitivity = 100f;
    // Variable that will store the actual player and allow for link between camera and the player
    public Transform playerBody; 
    // Variable that stores x rotation 
    private float xAxisRotation = 0f;

    // Start is called before the first frame update 
    void Start()
    {
        // Will lock the cursor so it is not visable while playing the game
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame 
    void Update()
    {
        // Will get and store the position of the mouse in terms of x and y axis
        // It will also multiply it by the mouse sensitivity and "Time.deltaTime" so the input is
        // correctly calcualted
        float mouseXAxis = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseYAxis = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // This will decrease the x rotation based on the y position of the mouse 
        xAxisRotation -= mouseYAxis;
        // This will clamp the roation so the player cannot look outside of the boundries specified 
        xAxisRotation = Mathf.Clamp(xAxisRotation, -90f, 90f);

        // Allows for rotation of the camera with the euler angles  
        transform.localRotation = Quaternion.Euler(xAxisRotation, 0f, 0f);

        // Rotates player body in the y axis when player moves mouse left and right
        playerBody.Rotate(Vector3.up * mouseXAxis);
    }
}
