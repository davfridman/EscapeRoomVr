using System.Collections;
using UnityEngine;

public class PeriscopeManager : MonoBehaviour
{
    [SerializeField] private bool isHoldingLeft = false;
    [SerializeField] private bool isHoldingRight = false;

    [SerializeField] private GameObject mainXROrigin;        // The XR Origin for the main camera
    [SerializeField] private GameObject islandXROrigin;      // The XR Origin for the island camera
    [SerializeField] private Camera transitionCamera;        // The transition camera

    [SerializeField] private float transitionDuration = 5f;  // Duration for the transition camera
    [SerializeField] private float islandDuration = 10f;     // Duration for the island camera

    private Coroutine cameraCycleCoroutine; // To keep track of the active coroutine

    // Start is called before the first frame update
    void Start()
    {
        if (mainXROrigin == null || islandXROrigin == null || transitionCamera == null)
        {
            Debug.LogError("XROrigins or Cameras are not assigned!");
            return;
        }

        // Ensure only the main XR Origin is visible at the start
        //EnableMainCamera();
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

            // Reset to the main XR Origin camera
            EnableMainCamera();
        }
    }

    // Coroutine to cycle through the cameras
    private IEnumerator CycleCameras()
    {
        while (true)
        {
            // Enable the transition camera
            EnableTransitionCamera();
            yield return new WaitForSeconds(transitionDuration);

            // Enable the island XR Origin's camera
            EnableIslandCamera();
            yield return new WaitForSeconds(islandDuration);

            // Return to the main XR Origin's camera
            EnableMainCamera();
        }
    }

    // Enable the main XR Origin's camera and disable the others
    private void EnableMainCamera()
    {
        SetCameraActive(mainXROrigin, true);
        SetCameraActive(islandXROrigin, false);
        transitionCamera.gameObject.SetActive(false);
    }

    // Enable the transition camera and disable the others
    private void EnableTransitionCamera()
    {
        SetCameraActive(mainXROrigin, false);
        SetCameraActive(islandXROrigin, false);
        transitionCamera.gameObject.SetActive(true);
    }

    // Enable the island XR Origin's camera and disable the others
    private void EnableIslandCamera()
    {
        SetCameraActive(mainXROrigin, false);
        SetCameraActive(islandXROrigin, true);
        transitionCamera.gameObject.SetActive(false);
    }

    // Helper method to enable or disable the XR Origin's camera while keeping the XR Origin active
    private void SetCameraActive(GameObject xrOrigin, bool isActive)
    {
        var camera = xrOrigin.GetComponentInChildren<Camera>(true); // Get the Camera component within the XR Origin
        if (camera != null)
        {
            camera.gameObject.SetActive(isActive);
        }
    }
}
