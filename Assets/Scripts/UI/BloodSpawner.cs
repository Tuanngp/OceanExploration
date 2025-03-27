using UnityEngine;

public class BloodSpawner : MonoBehaviour
{
    public static BloodSpawner Instance { get; private set; }
    public GameObject bloodPrefab;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SpawnBlood(Vector3 position)
    {
        if (bloodPrefab != null)
        {
            GameObject blood = Instantiate(bloodPrefab, position, Quaternion.identity);
            Destroy(blood, 10f); // Hủy máu sau 10 giây
        }
    }
}
