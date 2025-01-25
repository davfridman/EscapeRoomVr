using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit; // Make sure to include this if you're using XR Interaction Toolkit.

public class StartCutsceneScript : MonoBehaviour
{
    [SerializeField] private Camera cutsceneCamera; // Camera for the cutscene.
    [SerializeField] private Camera xrOrigin;   // XR Origin GameObject to enable after the cutscene.

    // Start is called before the first frame update
    void Start()
    {
        // Start the coroutine to handle the cutscene.
        StartCoroutine(HandleCutscene(4f));
    }

    // Coroutine to disable the camera and enable the XR Origin.
    IEnumerator HandleCutscene(float delay)
    {
        yield return new WaitForSeconds(delay); // Wait for the specified delay.

        // Disable the cutscene camera.
        if (cutsceneCamera != null)
        {
            cutsceneCamera.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Cutscene Camera not assigned in the Inspector!");
        }

        // Enable the XR Origin.
        if (xrOrigin != null)
        {
            xrOrigin.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogWarning("XR Origin not assigned in the Inspector!");
        }
    }
}
