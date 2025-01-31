using System.Collections;
using UnityEngine;
using UnityEngine.Playables;  // For Timeline control
using System.Collections.Generic;

public class StartCutsceneScript : MonoBehaviour
{
    [SerializeField] private PlayableDirector cutsceneDirector;  // Timeline's Playable Director
    [SerializeField] private Camera cutsceneCamera;              // Camera used for the cutscene
    [SerializeField] private Camera xrOrigin; 

    [Header("Objects to Control")]
    public List<GameObject> objectsToActivate;    // List of objects to activate
    public List<GameObject> objectsToDeactivate;


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
        DeactivateObjects();
        // Enable the main game camera
        if (xrOrigin != null)
        {
            xrOrigin.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogWarning("XR Origin Camera not assigned in the Inspector!");
        }
        ActivateObjects();
    }

    public void ActivateObjects()
    {
        foreach (GameObject obj in objectsToActivate)
        {
            if (obj != null)
            {
                obj.SetActive(true);
            }
        }
    }


    public void DeactivateObjects()
    {
        foreach (GameObject obj in objectsToDeactivate)
        {
            if (obj != null)
            {
                obj.SetActive(false);
            }
        }
    }
}
