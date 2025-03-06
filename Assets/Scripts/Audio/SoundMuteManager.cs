using UnityEngine;
using UnityEngine.UI;

public class SoundMuteManager : MonoBehaviour
{
    public AudioSource backgroundMusic; 
    public Button soundButton; 
    public Sprite soundOnSprite; 
    public Sprite soundOffSprite; 

    private bool isMuted = false; // Biến kiểm tra trạng thái âm thanh

    void Start()
    {
        
        isMuted = PlayerPrefs.GetInt("Muted", 0) == 1;
        UpdateSoundState();

        // Thêm sự kiện click vào button
        soundButton.onClick.AddListener(ToggleSound);
    }

    void ToggleSound()
    {
        isMuted = !isMuted; // Đảo trạng thái
        PlayerPrefs.SetInt("Muted", isMuted ? 1 : 0); // Lưu trạng thái
        PlayerPrefs.Save();
        UpdateSoundState();
    }

    void UpdateSoundState()
    {
        backgroundMusic.mute = isMuted; // Bật/tắt âm thanh
        soundButton.image.sprite = isMuted ? soundOffSprite : soundOnSprite; // Đổi hình ảnh button
    }
}
