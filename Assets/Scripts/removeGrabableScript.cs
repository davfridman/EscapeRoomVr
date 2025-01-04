using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class removeGrabable : MonoBehaviour
{
    [SerializeField] private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable xrGrabInteractable;
    [SerializeField] private GameObject waterEffect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DisableGrabable()
    {
        if (xrGrabInteractable != null)
            {
            if (xrGrabInteractable != null)
            {
                int defaultLayer = LayerMask.NameToLayer("Default");
                if (defaultLayer != -1)
                {
                    // Create an InteractionLayerMask without the default layer
                    xrGrabInteractable.interactionLayers &= ~UnityEngine.XR.Interaction.Toolkit.InteractionLayerMask.GetMask("Default");
                    Debug.Log("Default layer removed from interaction layers.");
                }
            }
                Destroy(waterEffect);
                Debug.Log("XRGrabInteractable has been disabled.");
            }
    }
}
