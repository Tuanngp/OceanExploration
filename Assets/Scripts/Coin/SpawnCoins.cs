using System.Collections;
using UnityEngine;

public class SpawnCoins : MonoBehaviour
{
    public GameObject coinPrefab;           // Coin bình thường
    public GameObject rareCoinPrefab;       // Coin đặc biệt
    public int numberOfCoins = 30;          // Tổng số coin thường cần spawn
    public int numberOfRareCoins = 10;      // Tổng số rare coin cần spawn
    public float minY = -50f, maxY = -10f;  // Giới hạn Y để spawn coin
    public float offsetX = 50f;              // Khoảng cách spawn theo X
    public Transform submarine;              // Tham chiếu tới tàu ngầm

    public float normalCoinInterval = 0.5f;  // Khoảng delay giữa mỗi coin thường
    public float rareCoinInterval = 2f;      // Khoảng delay giữa mỗi rare coin (tùy độ hiếm)

    private int normalCoinsSpawned = 0;
    private int rareCoinsSpawned = 0;

    void Start()
    {
        // Chạy 2 Coroutine song song, mỗi thằng tự lo việc spawn của mình
        StartCoroutine(SpawnNormalCoins());
        StartCoroutine(SpawnRareCoins());
    }

    IEnumerator SpawnNormalCoins()
    {
        while (normalCoinsSpawned < numberOfCoins)
        {
            SpawnCoin(coinPrefab);
            normalCoinsSpawned++;
            yield return new WaitForSeconds(normalCoinInterval);
        }
    }

    IEnumerator SpawnRareCoins()
    {
        while (rareCoinsSpawned < numberOfRareCoins)
        {
            // SpawnRareCoin();
            rareCoinsSpawned++;
            yield return new WaitForSeconds(rareCoinInterval);
        }
    }

    void SpawnCoin(GameObject coinType)
    {
        float randomX = submarine.position.x + Random.Range(-offsetX, offsetX);
        float randomY = Random.Range(minY, maxY);
        Vector2 position = new Vector2(randomX, randomY);
        Instantiate(coinType, position, Quaternion.identity);
    }

    void SpawnRareCoin()
    {
        if (SpawnMonsters.ActiveMonsters.Count == 0)
        {
            return;
        }

        MonsterMovement randomMonster = SpawnMonsters.ActiveMonsters[Random.Range(0, SpawnMonsters.ActiveMonsters.Count)];
        Vector2 spawnPos = (Vector2)randomMonster.transform.position + Random.insideUnitCircle * 10f;
        Instantiate(rareCoinPrefab, spawnPos, Quaternion.identity);
    }
}
