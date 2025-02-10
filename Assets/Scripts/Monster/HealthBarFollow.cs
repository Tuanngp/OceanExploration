using UnityEngine;

public class HealthBarFollow : MonoBehaviour
{
    private Transform object_monster;
    private Vector3 offset = new Vector3(0, 1.5f, 0); // Mặc định sát đầu quái vật

    public void SetTarget(Transform target, Vector3 customOffset)
    {
        object_monster = target;
        offset = customOffset;
    }

    void LateUpdate()
    {
        if (object_monster != null)
        {
            transform.position = object_monster.position + offset;
        }
    }
}
