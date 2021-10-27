using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

/// <summary>
/// This script is responsible for the statue puzzle with voice recognition.
/// </summary>
public class StatuesVoiceInteraction : MonoBehaviour
{
    private AudioSource audioSource;
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();
    private bool playerHasStatues;
    private bool isInRange;
    private GameObject pickedStatue;
    private int statueOnePositionNumber;
    private int statueTwoPositionNumber;
    private int statueThreePositionNumber;
    private int statueFourPositionNumber;

    public GameObject backpack;
    public GameObject statueOne;
    public GameObject statueTwo;
    public GameObject statueThree;
    public GameObject statueFour;
    public GameObject statueInventoryImage;
    public GameObject statueInventoryNumber;
    public GameObject paintings;
    public GameObject howToPlayMessage;
    // Stores material as a variable 
    public Material newMaterial;
    public Material oldMaterial;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        actions.Add("One", turnTopLeft);
        actions.Add("Two", turnTopRight);
        actions.Add("Three", turnBottomRight);
        actions.Add("Four", turnBottomLeft);
        actions.Add("Rotate statue", rotateStatue);

        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray(), ConfidenceLevel.Low);
        keywordRecognizer.OnPhraseRecognized += SpeechThatWasRecognized;
    }

    // Update is called once per frame
    void Update()
    {
        if (isInRange && !keywordRecognizer.IsRunning && playerHasStatues == true)
        { 
            keywordRecognizer.Start();
        }
        else if(keywordRecognizer.IsRunning && !isInRange)
        {
            keywordRecognizer.Stop();
        }
    }

    private void SpeechThatWasRecognized(PhraseRecognizedEventArgs speech)
    {
        actions[speech.text].Invoke();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = true;
        }
        // Runs method to check if player has all the required statues unless the player collided with the object before while having them
        if(playerHasStatues == false)
        {
            doesPlayerHasAllTheStatues();
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = false;
        }
    }

    /// <summary>
    /// Checks if player has all the statues within the back pack and if yes sets them on their correct position.
    /// It also gives player a message of how to control the puzzle and gets rid off the statues from inventory 
    /// ui. Then the information of player having all the statues is stored inside of the boolean variable. 
    /// </summary>
    private void doesPlayerHasAllTheStatues()
    {
        if (backpack.GetComponent<BackpackController>().statuesInBackpack == 3)
        {
            statueTwo.SetActive(true);
            statueThree.SetActive(true);
            statueFour.SetActive(true);
            howToPlayMessage.SetActive(true);
            statueInventoryImage.SetActive(false);
            statueInventoryNumber.SetActive(false);
            playerHasStatues = true;
        }
    }

    /// <summary>
    /// This method upon call will first reset the material of all statues to make sure that 
    /// no statue is selected visually. Then the picked statue variable is set to the variable that
    /// stores object of one of the four statues and sets that statue material to one that is red to 
    /// indicate to the player which statue they have selected. 
    /// </summary>
    private void turnTopLeft()
    {
        // Calls method which changes the material for all statues back to normal
        changeBackMaterial();
        // Sets the first statue as the picked one 
        pickedStatue = statueOne;
        // The picked statue color changes to red to show that this statue was selected by the player.
        pickedStatue.GetComponent<MeshRenderer>().material = newMaterial;
    }

    private void turnTopRight()
    {
        changeBackMaterial();
        pickedStatue = statueTwo;
        pickedStatue.GetComponent<MeshRenderer>().material = newMaterial;

    }

    private void turnBottomRight()
    {
        changeBackMaterial();
        pickedStatue = statueThree;
        pickedStatue.GetComponent<MeshRenderer>().material = newMaterial;
    }

    private void turnBottomLeft()
    {
        changeBackMaterial();
        pickedStatue = statueFour;
        pickedStatue.GetComponent<MeshRenderer>().material = newMaterial;
    }

    /// <summary>
    /// Once called this method rotates by 90 degress the z angle of the selected statue.
    /// Then it checks which statue is the selected one and for that it increases the counter 
    /// which keeps track of which direction the player is facing. Then the method calls a method
    /// that will check if the player has put all four statues in correct position. 
    /// </summary>
    private void rotateStatue()
    {
        audioSource.Play();
        pickedStatue.transform.eulerAngles = new Vector3(pickedStatue.transform.eulerAngles.x, pickedStatue.transform.eulerAngles.y, pickedStatue.transform.eulerAngles.z - 90);
        if (pickedStatue == statueOne)
        {
            if(statueOnePositionNumber < 3)
            {
                statueOnePositionNumber++;
            }
            else
            {
                statueOnePositionNumber = 0;
            }
        }
        else if(pickedStatue == statueTwo)
        {
            if(statueTwoPositionNumber < 3)
            {
                statueTwoPositionNumber++;
            }
            else
            {
                statueTwoPositionNumber = 0;
            }
        }
        else if (pickedStatue == statueThree)
        {
            if (statueThreePositionNumber < 3)
            {
                statueThreePositionNumber++;
            }
            else
            {
                statueThreePositionNumber = 0;
            }
        }
        else if (pickedStatue == statueFour)
        {
            if (statueFourPositionNumber < 3)
            {
                statueFourPositionNumber++;
            }
            else
            {
                statueFourPositionNumber = 0;
            }
        }
        isPuzzleSolved();
    }

    /// <summary>
    /// Method that changes the metarial of all the different statues 
    /// back to the original color so no statue is selected
    /// </summary>
    private void changeBackMaterial()
    {
        statueOne.GetComponent<MeshRenderer>().material = oldMaterial;
        statueTwo.GetComponent<MeshRenderer>().material = oldMaterial;
        statueThree.GetComponent<MeshRenderer>().material = oldMaterial;
        statueFour.GetComponent<MeshRenderer>().material = oldMaterial;
    }

    /// <summary>
    /// Method that checks if each statue is facing the correct way. If 
    /// yes it turns off the "how to player" message and displays the paitings 
    /// that represent the password for the door to the next room. 
    /// </summary>
    private void isPuzzleSolved()
    {
        if(statueOnePositionNumber == 3 && statueTwoPositionNumber == 3 && statueThreePositionNumber == 2 && statueFourPositionNumber == 2)
        {
            howToPlayMessage.SetActive(false);
            paintings.SetActive(true);
        }
    }
}
