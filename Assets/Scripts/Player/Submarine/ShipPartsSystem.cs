using UnityEngine;

public class ShipPartsSystem : MonoBehaviour
{
    public UpgradeManager playerData;
    public SpriteRenderer shipRenderer; 
    public Sprite[] shipParts; 

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