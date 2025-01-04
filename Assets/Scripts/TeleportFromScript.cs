using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportFromScript : MonoBehaviour
{
    [SerializeField] private Transform teleportTo;
    [SerializeField] private bool teleporterOnline = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateTeleporter() { teleporterOnline = true; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && teleporterOnline)
        {
            Debug.Log("teleporting");
            other.transform.position = teleportTo.position;
        }
    }
}