using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPuzzlesManagerScript : MonoBehaviour
{
    [SerializeField] private GameObject wallOpen;
    [SerializeField] private bool socketA = false;
    [SerializeField] private bool socketB = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SockPlaced()
    {
        socketA = true;
        OpenWall();
    }

    public void SockUnplaced() { socketA = false; }

    public void SockBPlaced()
    {
        socketB = true;
        OpenWall();
    }
    public void SockBUnplaced() { socketB = false; }

    private void OpenWall()
    {
        if (wallOpen == null) { return; }
        if (socketA && socketB) { Destroy(wallOpen); }
    }
}
