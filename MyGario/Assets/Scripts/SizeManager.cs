using UnityEngine;

public class SizeManager : MonoBehaviour
{
    private float _currentSize = 1f;
    public float scaleSpeed = 5f;

    void OnTriggerEnter2D(Collider2D other)
    {
        _currentSize *= 1.05f;
        Destroy(other.gameObject);
        
        GameManager.instance.SpawnFood();
    }

    void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(_currentSize, _currentSize, 1f), Time.deltaTime * scaleSpeed);
    }
}
