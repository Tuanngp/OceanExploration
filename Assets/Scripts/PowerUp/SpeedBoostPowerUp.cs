using UnityEngine;

public class SpeedBoostPowerUp : MonoBehaviour
{
    public float boostDuration = 5f;   // Thời gian tăng lực
    public float speedMultiplier = 2f; // Tăng gấp 2 tốc độ

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"PowerUp chạm vào: {other.gameObject.name}");

        if (other.CompareTag("Player"))
        {
            Debug.Log("PowerUp chạm đúng Player!");

            SpeedBoostHandler speedBoostHandler = other.GetComponent<SpeedBoostHandler>();

            if (speedBoostHandler != null)
            {
                Debug.Log("SpeedBoostHandler TÌM THẤY, kích hoạt boost!");
                speedBoostHandler.ActivateSpeedBoost(boostDuration, speedMultiplier);
            }
            else
            {
                Debug.LogWarning("Không tìm thấy SpeedBoostHandler trên Player.");
            }

            Destroy(gameObject);
        }
    }

}

