using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    // Variable that stores if the player is within a range of an object so he will be allowed to interact
    // with it
    public bool isInRange;
    // Will store the keyboard key that will trigger the interaction
    public KeyCode interactionKey;
    // Will store what interaction to perform
    public UnityEvent interaction;
    public GameObject interactionText;

    void Start()
    {

    }

    private void Update()
    {
        // Checks if the player is inside of the range to interact with the object
        if (isInRange)
        {
            // Checks if the button stored in the variable is pressed
            if (Input.GetKeyDown(interactionKey))
            {
                // If both tests pass all the events from event list will start to be performed
                interaction.Invoke();
            }
        }
    }

    /// <summary>
    /// This method will check if player is in range of the interactable object and if he is the 
    /// boolean variable will change to true
    /// </summary>
    /// <param name="collision">Object collider</param>
    private void OnTriggerEnter(Collider collision)
    {
        // Checks if the there is an object with tag "Player" that is colliding with the game object
        if (collision.gameObject.CompareTag("Player"))
        {
            // If the test passes the boolean variable will be changed to true.
            isInRange = true;
            interactionText.SetActive(true);

        }
    }

    /// <summary>
    /// This method will check if player is no longer in range of the interactable object and if he isn't the
    /// boolean variable will be set to false
    /// </summary>
    /// <param name="collision">Object collider</param>
    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = false;
            interactionText.SetActive(false);
        }
    }
}
