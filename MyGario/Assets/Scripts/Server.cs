using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Server : MonoBehaviour
{
    [SerializeField] private GameObject hostCanvas;
    [SerializeField] private GameObject guestCanvas;
    private bool _serverHasHost = false;
    
    
    private void HandleUI()
    {
        
        if (_serverHasHost)
        {
            guestCanvas.SetActive(true);
            hostCanvas.SetActive(false);
        }
        else
        {
            hostCanvas.SetActive(true);
            guestCanvas.SetActive(false);
        }
    }
    
    private void EstablishConnection()
    {
        //Get IP and username from clients through the host and guest Canvas textFields.
        //Make first client a host. 
        //Set _serverHasHost true after first client and until this connection is cancelled
        
    }
    
    
    private void SpawnPlayerInRandonLocations()
    {
        //Make sure hostCanvas and guestCanvas are inactive for each player right before they spawn.
        //After established connection, instantiate player prefab.
    }

    private void ReceiveData()
    {
        //Get speed and direction from each and every player every time
        //the speed or direction changes.
    }

    private void BroadcastReceivedData()
    {
        //Update the positions of the players based on their speed and direction.
    }

    private void Disconnection()
    {
        //If host disconnects, disconnect all clients.
    }
}
