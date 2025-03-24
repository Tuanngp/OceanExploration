using UnityEngine;
using UnityEngine.UI;

public class SaveSystemUI : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button saveButton;
    [SerializeField] private Button loadButton;
    [SerializeField] private Button deleteButton;
    
    [Header("Status Text")]
    [SerializeField] private Text statusText;
    
    private void Start()
    {
        // Initialize button listeners
        if (saveButton != null)
            saveButton.onClick.AddListener(SaveGame);
        
        if (loadButton != null)
            loadButton.onClick.AddListener(LoadGame);
        
        if (deleteButton != null)
            deleteButton.onClick.AddListener(DeleteSave);
        
        // Check if save exists
        UpdateStatusText();
    }
    
    public void SaveGame()
    {
        if (AutoSaveManager.Instance != null)
        {
            AutoSaveManager.Instance.SaveGame();
            UpdateStatusText("Game saved successfully!");
        }
        else
        {
            UpdateStatusText("Error: AutoSaveManager not found!");
        }
    }
    
    public void LoadGame()
    {
        if (AutoSaveManager.Instance != null)
        {
            if (SaveManager.SaveExists())
            {
                AutoSaveManager.Instance.LoadGame();
                UpdateStatusText("Game loaded successfully!");
            }
            else
            {
                UpdateStatusText("No save file found!");
            }
        }
        else
        {
            UpdateStatusText("Error: AutoSaveManager not found!");
        }
    }
    
    public void DeleteSave()
    {
        if (AutoSaveManager.Instance != null)
        {
            AutoSaveManager.Instance.DeleteSaveData();
            UpdateStatusText("Save data deleted!");
        }
        else
        {
            UpdateStatusText("Error: AutoSaveManager not found!");
        }
    }
    
    private void UpdateStatusText(string message = null)
    {
        if (statusText == null) return;
        
        if (message != null)
        {
            statusText.text = message;
        }
        else
        {
            // Check if save exists
            bool saveExists = SaveManager.SaveExists();
            statusText.text = saveExists ? "Save data found!" : "No save data found.";
        }
    }
} 