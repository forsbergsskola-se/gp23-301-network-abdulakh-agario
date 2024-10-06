using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class Server : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;  // Player prefab to spawn
    [SerializeField] private Transform[] spawnPoints;  // Random spawn points for players

    private UdpClient udpServer;
    private int serverPort = 9876; // Port for the server to listen on

    void Start()
    {
        // Initialize the UDP server and start listening for incoming connections
        udpServer = new UdpClient(serverPort);
        Debug.Log("Server started, listening on port: " + serverPort);

        // Start listening for clients in a non-blocking way (coroutine)
        StartCoroutine(ListenForClients());
    }

    // Coroutine to listen for clients and handle incoming data
    private IEnumerator ListenForClients()
    {
        while (true)
        {
            IPEndPoint clientEndPoint = new IPEndPoint(IPAddress.Any, 0);

            // Wait until a message is received from a client
            if (udpServer.Available > 0)
            {
                byte[] clientData = udpServer.Receive(ref clientEndPoint);
                string message = Encoding.UTF8.GetString(clientData);

                Debug.Log("Received message from client: " + message);
                
                // Spawn the player after receiving the message
                SpawnPlayer(clientEndPoint.Address.ToString(), message);
            }

            yield return null; // Wait for the next frame to continue
        }
    }

    // Method to spawn a player in a random location
    private void SpawnPlayer(string clientIpAddress, string userName)
    {
        // Get a random spawn point
        Vector3 spawnPosition = spawnPoints[Random.Range(0, spawnPoints.Length)].position;

        // Instantiate the player prefab at the spawn point
        GameObject player = Instantiate(playerPrefab, spawnPosition, Quaternion.identity);

        // Assign player data (username, IP) - assuming your PlayerController script has a method for this
        PlayerController playerController = player.GetComponent<PlayerController>();
        playerController.SetPlayerInfo(userName, clientIpAddress);

        Debug.Log($"Spawned player '{userName}' from IP {clientIpAddress} at {spawnPosition}");
    }

    private void OnApplicationQuit()
    {
        // Close the UDP server when the application quits
        if (udpServer != null)
        {
            udpServer.Close();
        }
    }
}
