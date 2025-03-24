using UnityEngine;
using System.IO;

public class SaveManager : MonoBehaviour
{
    private static string SavePath => Path.Combine(Application.persistentDataPath, "savedata.json");
    
    public static void SaveGame(SubmarineData data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(SavePath, json);
        Debug.Log("Game saved to: " + SavePath);
    }
    
    public static SubmarineData LoadGame()
    {
        if (File.Exists(SavePath))
        {
            string json = File.ReadAllText(SavePath);
            SubmarineData data = JsonUtility.FromJson<SubmarineData>(json);
            Debug.Log("Game loaded from: " + SavePath);
            return data;
        }
        else
        {
            Debug.Log("No save file found. Creating new game data.");
            return new SubmarineData();
        }
    }
    
    public static bool SaveExists()
    {
        return File.Exists(SavePath);
    }
    
    public static void DeleteSave()
    {
        if (File.Exists(SavePath))
        {
            File.Delete(SavePath);
            Debug.Log("Save file deleted.");
        }
    }
} 