using UnityEngine;

public class PetSpawner : MonoBehaviour
{
    public GameObject petPrefab; 
    public GameObject submarine; 
    public Vector3 petOffset = new Vector3(-3, -5, 0); 

    private GameObject spawnedPet;

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
        spawnedPet = Instantiate(petPrefab, submarine.transform.position + petOffset, Quaternion.identity);

        Debug.Log("Pet đã được spawn thành công tại vị trí của tàu ngầm.");
    }
}
