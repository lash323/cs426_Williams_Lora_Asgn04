using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Referenced to help write to this code:
// Trigger: https://www.youtube.com/watch?v=JHZN_vMqqr4&t=329s
// SetActive: https://www.youtube.com/watch?v=3PfpNuey3c0

public class IO_ButtonTrigger : MonoBehaviour
{
    public GameObject buttonText; // This is the object we are going to hide and unhide
    public int textNumber; // The number that will be displayed on buttonText
    public GameObject WinLoseManager;
    private WinLoseScript turnScript;
    TextMesh textMesh;

    public ParticleSystem particle_System;

    // This code will initialize everything
    void Start()
    {
        // Make sure the button text is hidden if not already
        if (buttonText != null)
        {
            textMesh = buttonText.GetComponent<TextMesh>();
            textMesh.text = "Blank";
            buttonText.SetActive(false);
            Debug.Log("Setting to IO_Room_Text to hidden");
        }
        turnScript = WinLoseManager.GetComponent<WinLoseScript>();
    }

    // OnTriggerEnter is called when another collider enters the trigger
    void OnTriggerEnter(Collider other)
    {
        if (buttonText != null && turnScript.turnNumber == 0)
        {
            turnScript.turnNumber++;
            textMesh = buttonText.GetComponent<TextMesh>();
            textMesh.text = "IO: " + textNumber;
            buttonText.SetActive(true); // Unhide text
            Debug.Log("Setting to IO_Room_Text to unhidden and updating number to " + textMesh.text);
            
            // string buttonName = transform.parent.gameObject.name;
            // if (buttonName == "PushButton_Mouse") {
            //     Debug.Log("Clicked the mouse button...");
                
            // }

            particle_System.Play();

            
        }
    }
}