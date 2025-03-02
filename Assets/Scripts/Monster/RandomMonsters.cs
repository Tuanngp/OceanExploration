using UnityEngine;
using System.Collections.Generic;

public class RandomMonsters : MonoBehaviour
{
    private GameObject currentMonster;
    [SerializeField] private List<GameObject> monsterPrefabs;
    private Transform modelTransform;

    void Awake() // Dùng Awake thay vì Start để đảm bảo mỗi Prefab spawn ra sẽ random quái mới
    {
        modelTransform = transform.Find("Model");
        if (modelTransform == null)
        {
            Debug.LogError("Không tìm thấy 'Model' trong Monster prefab!");
            return;
        }

        SpawnRandomMonster(); // Luôn spawn ngẫu nhiên mỗi lần có quái vật mới
    }

    private void SpawnRandomMonster()
    {
        if (monsterPrefabs.Count == 0) return;

        // Nếu Model đã có quái vật, xóa đi để spawn cái mới
        foreach (Transform child in modelTransform)
        {
            Destroy(child.gameObject);
        }

        int randomIndex = Random.Range(0, monsterPrefabs.Count);
        GameObject selectedMonster = monsterPrefabs[randomIndex];

        currentMonster = Instantiate(selectedMonster, modelTransform.position, Quaternion.identity);
        currentMonster.transform.SetParent(modelTransform);
    }
}
