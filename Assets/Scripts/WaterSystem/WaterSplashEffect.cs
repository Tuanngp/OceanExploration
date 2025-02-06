using UnityEngine;

public class WaterSplashEffect : MonoBehaviour
{
    public GameObject splashEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Rigidbody2D>())
        {
            Instantiate(splashEffect, collision.transform.position, Quaternion.identity);
        }
    }
}
