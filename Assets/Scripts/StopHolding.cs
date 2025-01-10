using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopHolding : MonoBehaviour
{
    [SerializeField] private Transform target;


    // Update is called once per frame
    public void GrabEnd()
    {
        transform.position = target.transform.position;
        transform.rotation = target.transform.rotation;
        transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);


    }
}
