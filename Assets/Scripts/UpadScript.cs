using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpadScript : MonoBehaviour
{
    [SerializeField] private bool isActivated = true;
    [SerializeField] private int currIsle = 0;
    [SerializeField] private AudioSource numberBeep;
    [SerializeField] private AudioSource ArivedFirstTime;
    [SerializeField] private AudioSource ArivedNormal;
    [SerializeField] private AudioSource OffLimits;
    [SerializeField] private FlagsManagerScript flagsManager;

    // Define allowed states
    private readonly HashSet<int> allowedIsleStates = new HashSet<int> { 0, 1, 2, 10, 11 };

    // Define a hash map from currIsle to active flag
    private readonly Dictionary<int, int> isleToFlagMap = new Dictionary<int, int>
    {
        { 1, 0 },
        { 2, 1 },
        { 0, 2 },
        { 11, 3 },
        { 10, 4 }
    };

    private bool hasMovedOnce = false; // Track if the player has moved at least once

    // Start is called before the first frame update
    void Start()
    {
        ActivateFlagForIsle();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Activate()
    {
        isActivated = true;
    }

    public void NumberButtonPressed(string buttonNum)
    {
        Debug.Log("Button pressed: " + buttonNum);
        if (!isActivated)
        {
            Debug.LogWarning("Cannot move: system is not activated.");
            return;
        }

        int newIsle = currIsle;

        // Update the currIsle based on the button pressed
        switch (buttonNum)
        {
            case "N":
                newIsle -= 10;
                break;
            case "S":
                newIsle += 10;
                break;
            case "E":
                newIsle += 1;
                break;
            case "W":
                newIsle -= 1;
                break;
            default:
                Debug.LogWarning("Invalid button pressed: " + buttonNum);
                return;
        }

        Debug.Log("New isle: " + newIsle);

        // Check if the newIsle is valid
        if (allowedIsleStates.Contains(newIsle))
        {
            currIsle = newIsle;
            Debug.Log("currIsle updated to: " + currIsle);
            ActivateFlagForIsle();
            PlayMovementSound();
        }
        else
        {
            Debug.LogWarning("Invalid movement. currIsle remains: " + currIsle);
            PlayOffLimitsSound();
        }
    }

    private void PlayMovementSound()
    {
        StopAllSounds();

        if (!hasMovedOnce)
        {
            // Play the "Arrived First Time" sound if this is the first movement
            ArivedFirstTime.Play();
            hasMovedOnce = true;
        }
        else
        {
            // Play the "Arrived Normal" sound for subsequent movements
            ArivedNormal.Play();
        }

        numberBeep.Play(); // Play sound feedback for button press
    }

    private void PlayOffLimitsSound()
    {
        StopAllSounds();
        OffLimits.Play();
    }

    private void StopAllSounds()
    {
        // Stop all sounds to ensure only one plays at a time
        numberBeep.Stop();
        ArivedFirstTime.Stop();
        ArivedNormal.Stop();
        OffLimits.Stop();
    }

    private void ActivateFlagForIsle()
    {
        if (isleToFlagMap.TryGetValue(currIsle, out int flagIndex))
        {
            flagsManager.ActivateFlag(flagIndex);
            Debug.Log("Flag activated for currIsle: " + currIsle + ", flag: " + flagIndex);
        }
        else
        {
            Debug.LogWarning("No flag mapping found for currIsle: " + currIsle);
        }
    }
}
