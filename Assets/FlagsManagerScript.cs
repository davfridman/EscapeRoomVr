using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagsManagerScript : MonoBehaviour
{
    // Array to store the flag GameObjects
    [SerializeField] private GameObject[] flags;

    // Start is called before the first frame update
    void Start()
    {
        // Optionally deactivate all flags at the start
        //DeactivateAllFlags();
    }

    // Function to activate a specific flag by index
    public void ActivateFlag(int index)
    {
        if (index < 0 || index >= flags.Length)
        {
            Debug.LogWarning("Invalid index: " + index);
            return;
        }

        DeactivateAllFlags(); // Ensure only one flag is active
        flags[index].SetActive(true); // Activate the specified flag
    }

    // Helper function to deactivate all flags
    private void DeactivateAllFlags()
    {
        foreach (GameObject flag in flags)
        {
            if (flag != null)
            {
                flag.SetActive(false);
            }
        }
    }
}
