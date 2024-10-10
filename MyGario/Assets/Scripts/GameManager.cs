using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    [Header("Player Spawning")]
    public GameObject player;

    public GameObject camera;
    
    [Header("Food Settings")]
    public GameObject foodPrefab;
    public Vector2 xRange;
    public Vector2 yRange;
    public float foodAmount = 29;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        for (int i = 0; i < foodAmount; i++)
        {
            SpawnFood();
        }
    }

    public void SpawnFood()
    {
        Vector3 spawnPosition = new Vector3(Random.Range(xRange.x, xRange.y), Random.Range(yRange.x, yRange.y), 1);
        GameObject food = Instantiate(foodPrefab, spawnPosition, Quaternion.identity);
        food.GetComponent<SpriteRenderer>().color = Random.ColorHSV(0f, 1f, 0.9f, 1f, 0.9f, 1f);
    }

    public void SpawnPlayer()
    {
        Vector3 spawnPosition = new Vector3(Random.Range(xRange.x, xRange.y), Random.Range(yRange.x, yRange.y), 1);
        GameObject _player = PhotonNetwork.Instantiate(player.name, spawnPosition, Quaternion.identity, 0);

        // Set a random color for the player
        SpriteRenderer playerRenderer = _player.GetComponent<SpriteRenderer>();
        if (playerRenderer != null)
        {
            playerRenderer.color = Random.ColorHSV(0f, 1f, 0.9f, 1f, 0.9f, 1f);
        }

        // Assign camera and enable movement & size logic
        camera.SetActive(true);
        camera.GetComponent<CameraFollow>().target = _player.transform;
        
        _player.GetComponent<SizeManager>().enabled = true;
        _player.GetComponent<PlayerMovement>().enabled = true;
    }
}