using TMPro;
using UnityEngine;

public class SizeManager : MonoBehaviour
{
    private float _currentSize = 1f;
    public float scaleSpeed = 5f;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Food"))
        {
            
            _currentSize *= 1.05f;
            Destroy(other.gameObject);
            GameManager.instance.SpawnFood();
        }
        else if (other.CompareTag("Player"))
        {
           
            SizeManager otherPlayer = other.GetComponent<SizeManager>();
            if (otherPlayer != null)
            {
                
                if (_currentSize > otherPlayer._currentSize)
                {
                    Destroy(other.gameObject);  
                }
                else if (_currentSize < otherPlayer._currentSize)
                {
                    Destroy(gameObject);  
                }
            }
        }
    }

    void Update()
    {
        // Scale the player smoothly over time
        transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(_currentSize, _currentSize, 1f), Time.deltaTime * scaleSpeed);
    }
}