using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricityPuzzleManagerScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject wallOpen;
    [SerializeField] private int numberOfSockets = 5;
    [SerializeField] private bool[] sockets;
    // Start is called before the first frame update
    void Start()
    {
        sockets = new bool[numberOfSockets];
        for (int i = 0; i < numberOfSockets; i++)
        {
            sockets[i] = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SockPlaced(GameObject socket)
    {
        // Get the XRSocketInteractor from the GameObject
        
        // Get the tag
        string socketTag = socket.tag;
        string plugTag = socket.GetComponent<SocketScript>().socketCheck();

        // Extract the last character of the tag and convert it to an integer
        int socketNum = GetSockNum(socketTag);
        int plugNum = GetSockNum(plugTag);
        int socketIndex = socketNum - 1;
        int plugIndex = plugNum - 1;
        //Debug.Log("sockNum: " + sockIndex);

        // Check if socket is legal
        if (!CheckSocketIndexValid(plugIndex) || !!CheckSocketIndexValid(socketIndex) || plugNum != socketNum) {return;}

        // Update socket status
        sockets[socketIndex] = true;
        CheckAllSockets();
    }

    public int GetSockNum(string sockTag)
    {
        // Extract the last character of the tag and convert it to an integer
        if (!string.IsNullOrEmpty(sockTag) && char.IsDigit(sockTag[sockTag.Length - 1]))
        {
            int sockTagNum = int.Parse(sockTag[sockTag.Length - 1].ToString());
            //Debug.Log("Last character of tag as int: " + sockTagNum);
            return sockTagNum;
        }
        Debug.LogWarning("Tag is empty or does not end with a digit.");
        return 0;
    }

    public bool CheckSocketIndexValid(int sockIndex)
    {
        if (sockIndex == -1) {return false;}
        if (sockIndex >= numberOfSockets) {return false;}
        return true;
    }

    public void SockUnplaced(GameObject socket) 
    { 
        // Get the tag
        string tag = socket.tag;

        // Extract the last character of the tag and convert it to an integer
        int sockNum = GetSockNum(tag);
        int sockIndex = sockNum - 1;
        //Debug.Log("sockNum: " + sockIndex);

        // Check if socket is legal
        if (!CheckSocketIndexValid(sockIndex)) {return;}

        // Update socket status
        sockets[sockIndex] = false;

    }

    private void CheckAllSockets()
    {
        if (wallOpen == null) { return; }

        // Check if all sockets are true
        for (int i = 0; i < numberOfSockets; i++)
        {
            if (!sockets[i]) { return; }
        }
        Destroy(wallOpen);
    }
}
