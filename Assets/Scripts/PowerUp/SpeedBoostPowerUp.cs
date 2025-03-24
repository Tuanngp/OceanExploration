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

            SpeedBoostHandler speedBoostHandler = other.GetComponent<SpeedBoostHandler>();

            if (speedBoostHandler != null)
            {
                speedBoostHandler.ActivateSpeedBoost(boostDuration, speedMultiplier);
            }

            Destroy(gameObject);
        }
    }

}

