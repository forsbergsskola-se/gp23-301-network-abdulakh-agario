using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Camera cam;
    public float moveSpeed = 3f;

    // Define the boundaries of the map
    public float minX = -10f;
    public float maxX = 10f;
    public float minY = -10f;
    public float maxY = 10f;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        // Get the mouse position in world coordinates
        Vector2 input = Input.mousePosition;
        Vector2 worldInput = cam.ScreenToWorldPoint(input);

        // Move the player towards the mouse position
        transform.position = Vector3.MoveTowards(transform.position, worldInput, moveSpeed * Time.deltaTime);

        // Clamp the player's position to stay within the defined grid boundaries
        float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);
        float clampedY = Mathf.Clamp(transform.position.y, minY, maxY);

        // Apply the clamped position back to the player
        transform.position = new Vector2(clampedX, clampedY);
    }
}