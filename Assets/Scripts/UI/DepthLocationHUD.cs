using UnityEngine;
using TMPro;

public class DepthLocationHUD : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI depthText;
    [SerializeField] private TextMeshProUGUI locationText;

    // [Header("Zone Settings")]
    [System.Serializable]
    public class OceanZone
    {
        public string zoneName;
        public float minDepth;
        public float maxDepth;
        public Color zoneColor = Color.white;
    }

    [SerializeField] private OceanZone[] oceanZones;

    [Header("Depth Display Settings")]
    [SerializeField] private string depthUnit = "m";
    [SerializeField] private bool roundDepthToInt = true;

    private Transform submarineTransform;
    private float currentDepth;
    private string currentZone;

    private void Start()
    {
        submarineTransform = GameObject.FindGameObjectWithTag("Player").transform;

        if (submarineTransform == null)
            Debug.LogError("Submarine not found! Make sure it has the 'Player' tag.");

        UpdateDepthAndLocation();
    }

    private void Update()
    {
        UpdateDepthAndLocation();
    }

    private void UpdateDepthAndLocation()
    {
        if (submarineTransform == null) return;

        // Tính độ sâu (giả sử Y = 0 là mặt nước)
        currentDepth = Mathf.Abs(submarineTransform.position.y);

        string depthDisplay;
        if (roundDepthToInt)
            depthDisplay = $"Depth: {Mathf.RoundToInt(currentDepth)}{depthUnit}";
        else
            depthDisplay = $"Depth: {currentDepth:F1}{depthUnit}";

        depthText.text = depthDisplay;

        UpdateCurrentZone();
    }

    private void UpdateCurrentZone()
    {
        foreach (OceanZone zone in oceanZones)
        {
            if (currentDepth >= zone.minDepth && currentDepth <= zone.maxDepth)
            {
                currentZone = zone.zoneName;
                locationText.text = $"Location: {currentZone}";
                // locationText.color = zone.zoneColor;
                return;
            }
        }
    }
}