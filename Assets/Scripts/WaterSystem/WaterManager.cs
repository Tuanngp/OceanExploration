using UnityEngine;

public class WaterManager : MonoBehaviour
{
    public static WaterManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void RegisterWater(Water water)
    {
        Debug.Log("Water registered: " + water.name);
    }
}
