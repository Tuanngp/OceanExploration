using System.Collections;
using UnityEngine;

public class SpawnCoins : MonoBehaviour
{
    public GameObject coinPrefab;  
    public int numberOfCoins = 5;
    public float minX = 320f, maxX = 350f;
    public float minY = 230f, maxY = 247f;

    void Start()
    {
        SpawnRandomCoins();
    }

    void SpawnRandomCoins()
    {
        for (int i = 0; i < numberOfCoins; i++)
        {
            float randomX = Random.Range(minX, maxX);
            float randomY = Random.Range(minY, maxY);
            Vector2 randomPosition = new Vector2(randomX, randomY);

            Instantiate(coinPrefab, randomPosition, Quaternion.identity);
        }
    }
}
