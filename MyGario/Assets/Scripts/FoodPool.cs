using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodPool : MonoBehaviour
{
    public GameObject foodPrefab;  // Reference to your food prefab
    public int maxFoodCount = 100;  // Maximum food items to spawn
    public float spawnRadius = 50f; // Radius of the map (half of 100x100)
    public float refillTime = 5f;   // Time interval to refill the pool

    private List<GameObject> foodPool = new List<GameObject>(); // List to store food instances

    // Colors for the food items
    private Color[] foodColors = { Color.red, Color.blue, Color.green, Color.yellow };

    void Start()
    {
        // Initially spawn all food items
        SpawnInitialFood();

        // Start refilling the pool periodically
        StartCoroutine(RefillFood());
    }

    // Spawns the initial set of food items
    private void SpawnInitialFood()
    {
        for (int i = 0; i < maxFoodCount; i++)
        {
            SpawnFood();
        }
    }

    // Refill the pool every 'refillTime' seconds
    private IEnumerator RefillFood()
    {
        while (true)
        {
            yield return new WaitForSeconds(refillTime);

            // Check if the pool has less than 30 items, and spawn more if needed
            if (foodPool.Count < maxFoodCount)
            {
                int foodToSpawn = maxFoodCount - foodPool.Count;
                for (int i = 0; i < foodToSpawn; i++)
                {
                    SpawnFood();
                }
            }
        }
    }

    // Spawn a single food item at a random position with a random color
    private void SpawnFood()
    {
        // Generate random position within the map radius
        Vector3 randomPosition = new Vector3(
            Random.Range(-spawnRadius, spawnRadius),
            Random.Range(-spawnRadius, spawnRadius),
            0 // Assuming the map is flat (Z = 0 for 2D plane)
        );

        // Instantiate the food prefab
        GameObject food = Instantiate(foodPrefab, randomPosition, Quaternion.identity);

        // Assign a random color to the food
        SpriteRenderer foodRenderer = food.GetComponent<SpriteRenderer>();
        if (foodRenderer != null)
        {
            Color randomColor = GetRandomColor();
            foodRenderer.color = randomColor;

            // Log the color to verify
            Debug.Log("Assigned color: " + randomColor.ToString());
        }
        else
        {
            Debug.LogError("SpriteRenderer not found on the foodPrefab.");
        }

        // Add it to the pool
        foodPool.Add(food);
    }

    // Get a random color from the predefined color array
    private Color GetRandomColor()
    {
        return foodColors[Random.Range(0, foodColors.Length)];
    }

    // This method can be used to remove food from the pool if needed
    public void RemoveFood(GameObject food)
    {
        if (foodPool.Contains(food))
        {
            foodPool.Remove(food);
            Destroy(food);
        }
    }
}
