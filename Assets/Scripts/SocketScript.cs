using System.Collections;
using System.Collections.Generic;
using UnityEngine;
  // Add this directive for clarity

public class SocketScript : MonoBehaviour
{
    UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor socket;

    void Start()
    {
        socket = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor>();
    }

    public void socketCheck()
    {
        UnityEngine.XR.Interaction.Toolkit.Interactables.IXRSelectInteractable objName = socket.firstInteractableSelected; // Updated reference

        if (objName != null)  // Ensure it's not null before accessing properties
        {
            Debug.Log(objName.transform.tag + " in socket of " + transform.name);
        }
        else
        {
            Debug.Log("No object in the socket of " + transform.name);
        }
    }
}