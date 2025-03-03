using UnityEngine;

public class PowerUpRotation : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(Vector3.forward * 50f * Time.deltaTime);  // Xoay nhẹ
    }
}
