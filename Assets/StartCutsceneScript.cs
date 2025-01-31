using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit; // Make sure to include this if you're using XR Interaction Toolkit.
using UnityEngine.Playables;  // For Timeline control

public class StartCutsceneScript : MonoBehaviour
{
    [SerializeField] private PlayableDirector cutsceneDirector;  // Timeline's Playable Director
    [SerializeField] private Camera cutsceneCamera;              // Camera used for the cutscene
    [SerializeField] private Camera xrOrigin;                    // Main XR or game camera to activate after the cutscene

    void Start()
    {
        // Start the cutscene by playing the Timeline
        if (cutsceneDirector != null)
        {
            cutsceneDirector.Play();  // Play the timeline
            StartCoroutine(HandleCutscene((float)cutsceneDirector.duration));  // Handle camera transition based on timeline duration
        }
        else
        {
            Debug.LogError("PlayableDirector is not assigned!");
        }
    }

    IEnumerator HandleCutscene(float delay)
    {
        // Wait for the timeline duration
        yield return new WaitForSeconds(delay);

        // Disable the cutscene camera
        if (cutsceneCamera != null)
        {
            cutsceneCamera.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Cutscene Camera not assigned in the Inspector!");
        }

        // Enable the main game camera
        if (xrOrigin != null)
        {
            xrOrigin.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogWarning("XR Origin Camera not assigned in the Inspector!");
        }
    }
}

