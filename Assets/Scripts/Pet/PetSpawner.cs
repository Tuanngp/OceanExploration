using UnityEngine;

public class PetSpawner : MonoBehaviour
{
    public GameObject petPrefab; // Prefab của Pet cần spawn
    public GameObject submarine; // Tham chiếu đến tàu ngầm mà Pet sẽ đi theo
    public Vector3 petOffset = new Vector3(-3, -5, 0); // Vị trí lệch của Pet

    private GameObject spawnedPet; // Biến lưu trữ Pet đã được spawn

    void Start()
    {
        if (petPrefab == null)
        {
            Debug.LogError("Chưa gán Prefab Pet vào script PetSpawner!");
            return;
        }

        if (submarine == null)
        {
            Debug.LogError("Chưa gán Submarine vào script PetSpawner!");
            return;
        }

        // Spawn Pet tại vị trí tàu ngầm + offset
        spawnedPet = Instantiate(petPrefab, submarine.transform.position + petOffset, Quaternion.identity);

        Debug.Log("Pet đã được spawn thành công tại vị trí của tàu ngầm.");
    }
}
