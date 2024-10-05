using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;  // Movement speed of the player
    private Vector3 _direction;
    private Camera _mainCamera;

    void Start()
    {
        _mainCamera = Camera.main;
    }

    void Update()
    {
        // Get the world position of the mouse
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = _mainCamera.ScreenToWorldPoint(mousePosition);
        mousePosition.z = 0f; // Ignore the z-axis for 2D movement

        // Move the player towards the mouse position
        _direction = (mousePosition - transform.position).normalized;
        transform.position += _direction * moveSpeed * Time.deltaTime;
    }
}