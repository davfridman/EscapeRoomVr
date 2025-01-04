using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class removeGrabable : MonoBehaviour
{
    [SerializeField] private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable xrGrabInteractable;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DisableGrabable(){
        if (xrGrabInteractable != null)
            {
                xrGrabInteractable.enabled = false;
                Debug.Log("XRGrabInteractable has been disabled.");
            }
    }
}
