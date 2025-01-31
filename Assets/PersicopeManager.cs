using UnityEngine;
using System.Collections;

public class PeriscopeManager : MonoBehaviour
{
    [SerializeField] private bool isHoldingLeft = false;
    [SerializeField] private bool isHoldingRight = false;

    [SerializeField] private GameObject regularCamera;       // Main camera
    [SerializeField] private GameObject islandCamera;        // Island camera
    [SerializeField] private GameObject periscopeParent;     // Periscope object with Animator
    [SerializeField] private Camera transitionCamera;        // Static camera for the animation transition

    [SerializeField] private float animationDuration = 5f;   // Duration of the periscope animations

    private Coroutine cameraCycleCoroutine;
    private Animator periscopeAnimator;

    void Start()
    {
        if (regularCamera == null || islandCamera == null || periscopeParent == null || transitionCamera == null)
        {
            Debug.LogError("Cameras or Periscope Parent are not assigned!");
            return;
        }

        periscopeAnimator = periscopeParent.GetComponent<Animator>();
        if (periscopeAnimator == null)
        {
            Debug.LogError("Animator component missing on Periscope Parent!");
        }

        EnableRegularCamera();
    }

    public void hold(bool isLeft)
    {
        if (isLeft) isHoldingLeft = true;
        else isHoldingRight = true;

        if (isHoldingLeft && isHoldingRight && cameraCycleCoroutine == null)
        {
            cameraCycleCoroutine = StartCoroutine(HandlePeriscopePickup());
        }
    }

    public void unhold(bool isLeft)
    {
        if (isLeft) isHoldingLeft = false;
        else isHoldingRight = false;

        if (!isHoldingLeft && !isHoldingRight && cameraCycleCoroutine == null)
        {
            cameraCycleCoroutine = StartCoroutine(HandlePeriscopeRelease());
        }
    }

    private IEnumerator HandlePeriscopePickup()
    {
        // Enable the transition camera to start the animation
        EnableTransitionCamera();

        // Trigger the "PeriscopeUp" animation
        if (periscopeAnimator != null)
        {
            periscopeAnimator.SetTrigger("StartCamera");
        }

        // Wait for the animation to complete
        yield return new WaitForSeconds(animationDuration);

        // Switch to the island camera after the animation
        EnableIslandCamera();

        cameraCycleCoroutine = null;  // Reset coroutine state
    }

    private IEnumerator HandlePeriscopeRelease()
    {
        // Enable the transition camera for the reverse animation
        EnableTransitionCamera();

        // Trigger the "PeriscopeDown" animation
        if (periscopeAnimator != null)
        {
            periscopeAnimator.SetTrigger("EndCamera");
        }

        // Wait for the animation to complete
        yield return new WaitForSeconds(animationDuration);

        // Switch back to the regular camera
        EnableRegularCamera();

        cameraCycleCoroutine = null;  // Reset coroutine state
    }

    private void EnableRegularCamera()
    {
        SetCameraActive(regularCamera, true);
        SetCameraActive(islandCamera, false);
        transitionCamera.gameObject.SetActive(false);
    }

    private void EnableIslandCamera()
    {
        SetCameraActive(regularCamera, false);
        SetCameraActive(islandCamera, true);
        transitionCamera.gameObject.SetActive(false);
    }

    private void EnableTransitionCamera()
    {
        SetCameraActive(regularCamera, false);
        SetCameraActive(islandCamera, false);
        transitionCamera.gameObject.SetActive(true);
    }

    private void SetCameraActive(GameObject cameraObject, bool isActive)
    {
        var camera = cameraObject.GetComponentInChildren<Camera>(true);
        if (camera != null)
        {
            camera.gameObject.SetActive(isActive);
        }
    }
}
