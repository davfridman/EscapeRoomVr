using System.Collections;
using UnityEngine;

public class FlickerLight : MonoBehaviour
{
    [SerializeField] private Light lightSource;    // Reference to the Light component
    [SerializeField] private bool isFlickering = false;  // Whether the light should flicker
    [SerializeField] private float flickerSpeed = 0.1f;  // Speed at which the flicker happens
    [SerializeField] private float flickerIntensityMin = 0.1f;  // Minimum intensity during flicker
    [SerializeField] private float flickerIntensityMax = 1f;    // Maximum intensity during flicker

    private float originalIntensity;  // To store the original intensity of the light

    // Start is called before the first frame update
    void Start()
    {
        if (lightSource == null)
        {
            lightSource = GetComponent<Light>();  // Get Light component if not manually assigned
        }

        if (lightSource != null)
        {
            originalIntensity = lightSource.intensity;  // Store the light's original intensity
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isFlickering)
        {
            Flicker();
        }
        else
        {
            // Reset to normal intensity when flickering is off
            if (lightSource != null)
            {
                lightSource.intensity = originalIntensity;
            }
        }
    }

    // Method to handle the light flickering effect
    void Flicker()
    {
        if (lightSource != null)
        {
            // Randomly set the light intensity between the min and max values
            lightSource.intensity = Mathf.Lerp(flickerIntensityMin, flickerIntensityMax, Mathf.PingPong(Time.time * flickerSpeed, 1));
        }
    }
}
