using UnityEngine;

public class ShipPartsSystem : MonoBehaviour
{
    public UpgradeManager playerData;
    public SpriteRenderer shipRenderer; // Hiển thị sprite của tàu
    public Sprite[] shipParts; // Danh sách các sprite của bộ phận tàu

    public void NextPart()
    {
        playerData.currentShipPartIndex = (playerData.currentShipPartIndex + 1) % shipParts.Length;
        UpdateShipPart();
    }

    public void PreviousPart()
    {
        playerData.currentShipPartIndex = (playerData.currentShipPartIndex - 1 + shipParts.Length) % shipParts.Length;
        UpdateShipPart();
    }

    private void UpdateShipPart()
    {
        shipRenderer.sprite = shipParts[playerData.currentShipPartIndex];
    }
}