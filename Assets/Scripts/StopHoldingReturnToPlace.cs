using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopHoldingReturnToPlace : MonoBehaviour
{
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private Vector3 initialScale;

    // Start is called before the first frame update
    void Start()
    {
        // Save the initial values of position, rotation, and scale
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        initialScale = transform.localScale;
    }

    // Method to return to the initial values
    public void GrabEnd()
    {
        transform.position = initialPosition;
        transform.rotation = initialRotation;
        transform.localScale = initialScale;
    }
}
