using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterialState : MonoBehaviour
{
    [SerializeField] private Material NewMaterial;
    [SerializeField] private Renderer objectRenderer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeMaterial(){
        objectRenderer.material = NewMaterial;
    }
}
