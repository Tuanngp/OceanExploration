using System.Collections;
using UnityEngine;

public class SpawnCoins : MonoBehaviour
{
    public GameObject coinPrefab;
    public int numberOfCoins = 50;
    public float minY = 345f, maxY = 398f;
    public float offsetX = 50f; // Khoảng cách X để spawn quanh tàu
    public Transform submarine; // Tham chiếu đến tàu ngầm
    public float spawnInterval = 0.5f; // Thời gian giữa mỗi lần tạo coin

    void Start()
    {
        StartCoroutine(SpawnCoinsGradually());
    }

    IEnumerator SpawnCoinsGradually()
    {
        for (int i = 0; i < numberOfCoins; i++)
        {
            float randomX = submarine.position.x + Random.Range(-offsetX, offsetX);
            float randomY = Random.Range(minY, maxY);
            Vector2 randomPosition = new Vector2(randomX, randomY);

            Instantiate(coinPrefab, randomPosition, Quaternion.identity);

            yield return new WaitForSeconds(spawnInterval); // Đợi trước khi tạo coin tiếp theo
        }
    }
}