using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script will allow for teleporting player from one location
/// within the game to another. 
/// </summary>
public class TeleportingScript : MonoBehaviour
{
    // Stores true once the player enters through portal and false when teleportation is done
    private bool isPlayerGoingThroughPortal;

    // Stores link to the player game object
    public CharacterController player;
    // Stores link to the portal that will pop out the player
    public Transform portalReceiver;

    // Update is called once per frame
    void Update()
    {
        if (isPlayerGoingThroughPortal)
        {
            // Stores vector that will point towards the player from the portal
            Vector3 portalToPlayer = player.transform.position - transform.position;
            // Computes and stores dot product using the vector pointing from portal up and a vector point from portal toward the player
            float dotProduct = Vector3.Dot(transform.up, portalToPlayer);

            // Checks if player walked through the portal by checking if the dot product is below zero
            if (dotProduct < 0f)
            {
                // Calculates the reversed rotation difference between rotation of the portal and the receiver portal
                float rotationDifference = -Quaternion.Angle(transform.rotation, portalReceiver.rotation);
                // Adds ninety digrees to angle of rotation so that the player looks the correct way when walks out of the portal
                rotationDifference += 90;
                // Rotates the player in the up axis based on the calcuated rotation difference
                player.transform.Rotate(Vector3.up, rotationDifference);

                // Calculates and stores the position offset by creating rotation in y axis by the calculated
                // rotation difference and multiplaying it by the previously calcualted vector
                Vector3 positionOffset = Quaternion.Euler(0f, rotationDifference, 0f) * portalToPlayer;
                
                // Turns off player object collider so player doesn't collide with any game object during teleportation
                player.enabled = false;
                // Changes player position based on the receiver portal and the possition off set of the player calcualted before
                player.transform.position = portalReceiver.position + positionOffset;
                // Turns back on player object collider
                player.enabled = true;

                // As player was teleported the boolean is set back to false
                isPlayerGoingThroughPortal = false;
            }   
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerGoingThroughPortal = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerGoingThroughPortal = false;
        }
    }
}
