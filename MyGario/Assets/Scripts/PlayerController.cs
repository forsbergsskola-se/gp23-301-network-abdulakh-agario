using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Vector3 _direction;
    private Camera _mainCamera;

    private string _userName;
    private string _IpAddress;

    private UdpClient udpClient;
    private string serverIp = "127.0.0.1"; // Server IP for testing (localhost)
    private int serverPort = 9876;         // Server port

    void Start()
    {
        _mainCamera = Camera.main;

        // Initialize UDP client
        udpClient = new UdpClient();
        
        // Optionally: Send initial connection data to the server here
        ConnectToServer();
    }

    void Update()
    {
        // Handle player movement
        HandleMovement();

        // Send player movement data to the server
        SendMovementData();
    }

    private void HandleMovement()
    {
        // Get mouse position and move the player
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = _mainCamera.ScreenToWorldPoint(mousePosition);
        mousePosition.z = 0f;

        // Calculate direction and move the player
        _direction = (mousePosition - transform.position).normalized;
        transform.position += _direction * moveSpeed * Time.deltaTime;
    }

    private void ConnectToServer()
    {
        // This method is for sending initial data, such as username, to the server
        string message = _userName + "|" + _IpAddress;
        byte[] messageBytes = Encoding.UTF8.GetBytes(message);

        try
        {
            udpClient.Send(messageBytes, messageBytes.Length, serverIp, serverPort);
            Debug.Log("Sent initial connection message to server: " + message);
        }
        catch (SocketException e)
        {
            Debug.LogError("Error sending message to server: " + e.Message);
        }
    }

    private void SendMovementData()
    {
        // Send current player movement data (like position and direction) to the server
        Vector3 position = transform.position;
        string movementData = _userName + "|" + position.x + "," + position.y + "," + position.z + "|" + _direction.x + "," + _direction.y + "," + _direction.z;

        byte[] movementDataBytes = Encoding.UTF8.GetBytes(movementData);

        try
        {
            udpClient.Send(movementDataBytes, movementDataBytes.Length, serverIp, serverPort);
            Debug.Log("Sent movement data to server: " + movementData);
        }
        catch (SocketException e)
        {
            Debug.LogError("Error sending movement data to server: " + e.Message);
        }
    }

    public void SetPlayerInfo(string userName, string ipAddress)
    {
        _userName = userName;
        _IpAddress = ipAddress;
    }

    private void OnApplicationQuit()
    {
        // Close the UDP client when the application quits
        if (udpClient != null)
        {
            udpClient.Close();
        }
    }
}
