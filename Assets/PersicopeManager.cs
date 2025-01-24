using System.Collections;
using UnityEngine;

public class PeriscopeManager : MonoBehaviour
{
    [SerializeField] private bool isHoldingLeft = false;
    [SerializeField] private bool isHoldingRight = false;
    [SerializeField] private Camera mainCamera;          // The main camera
    [SerializeField] private Camera transitionCamera;    // The transition camera
    [SerializeField] private Camera islandCamera;        // The island camera
    [SerializeField] private float transitionDuration = 5f;  // Duration for the transition camera
    [SerializeField] private float islandDuration = 1000f;     // Duration for the island camera

    private Coroutine cameraCycleCoroutine; // To keep track of the active coroutine

    // Start is called before the first frame update
    void Start()
    {
        if (mainCamera == null || transitionCamera == null || islandCamera == null)
        {
            Debug.LogError("Cameras are not assigned!");
            return;
        }

        // Ensure only the main camera is active at the start
        mainCamera.gameObject.SetActive(true);
        transitionCamera.gameObject.SetActive(false);
        islandCamera.gameObject.SetActive(false);
    }

    // Call this method to start the camera cycle process when both hands are holding
    public void hold(bool isLeft)
    {
        if (isLeft)
        {
            isHoldingLeft = true;
        }
        else
        {
            isHoldingRight = true;
        }

        if (isHoldingLeft && isHoldingRight && cameraCycleCoroutine == null)
        {
            cameraCycleCoroutine = StartCoroutine(CycleCameras());
        }
    }

    // Call this method to stop the process when a hand is released
    public void unhold(bool isLeft)
    {
        if (isLeft)
        {
            isHoldingLeft = false;
        }
        else
        {
            isHoldingRight = false;
        }

        if (!isHoldingLeft && !isHoldingRight && cameraCycleCoroutine != null)
        {
            StopCoroutine(cameraCycleCoroutine);
            cameraCycleCoroutine = null;

            // Reset to main camera
            SetActiveCamera(mainCamera);
        }
    }

    // Coroutine to cycle through the cameras
    private IEnumerator CycleCameras()
    {
        while (true)
        {
            // Show the transition camera
            SetActiveCamera(transitionCamera);
            yield return new WaitForSeconds(transitionDuration);

            // Show the island camera
            SetActiveCamera(islandCamera);
            yield return new WaitForSeconds(islandDuration);

            // Return to the main camera
            SetActiveCamera(mainCamera);
        }
    }

    // Helper function to activate one camera and deactivate the others
    private void SetActiveCamera(Camera activeCamera)
    {
        mainCamera.gameObject.SetActive(activeCamera == mainCamera);
        transitionCamera.gameObject.SetActive(activeCamera == transitionCamera);
        islandCamera.gameObject.SetActive(activeCamera == islandCamera);
    }
}
