using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Camera cam;
    public float moveSpeed = 3f;
    

    void Start()
    {
        cam = Camera.main;
    }
    void Update()
    {
        Vector2 input = Input.mousePosition;
        Vector2 worldInput = cam.ScreenToWorldPoint(input);
        transform.position = Vector3.MoveTowards(transform.position, worldInput, moveSpeed * Time.deltaTime);
    }
}
