using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


public class GrabLastCubeScript : MonoBehaviour
{
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;
    private Rigidbody rb;

    void Start()
    {
        // Get components
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        rb = GetComponent<Rigidbody>();

        if (grabInteractable == null || rb == null)
        {
            Debug.LogError("Missing required components!");
            return;
        }

        // Start with the interactable disabled
        grabInteractable.enabled = false;

        // Disable physics
        rb.isKinematic = true;
        rb.useGravity = false;
    }

    void Update()
    {
        // Check the static variable from GameStateManager
        if (NumberPadButtonLastScript.lastPuzzleDone && !grabInteractable.enabled)
        {
            // Enable grab interactable when puzzle is completed
            grabInteractable.enabled = true;
            Debug.Log("Object is now grabable.");
        }
    }

    // This function should be assigned to the Select Entered event in the Unity Editor
    public void OnGrabbed(UnityEngine.XR.Interaction.Toolkit.SelectEnterEventArgs args)
    {
        // Attach the object to the hand that grabbed it
        Transform attachedHand = args.interactorObject.transform;

        // Disable physics to make it part of the hand
        rb.isKinematic = true;
        rb.useGravity = false;
        rb.detectCollisions = false;

        // Parent the object to the hand
        transform.SetParent(attachedHand);

        // Disable further interaction
        grabInteractable.enabled = false;

        Debug.Log("Object grabbed and attached to hand.");
    }
}
