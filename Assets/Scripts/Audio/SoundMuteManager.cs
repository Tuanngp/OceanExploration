using UnityEngine;
using UnityEngine.UI;

public class SoundMuteManager : MonoBehaviour
{
    private AudioSource backgroundMusic; 
    public Button soundButton; 
    public Sprite soundOnSprite; 
    public Sprite soundOffSprite; 

    private bool isMuted = false;

    void Start()
    {
        var backgroundMusicObject = GameObject.Find("BackgroundMusic");
        if (backgroundMusicObject != null)
        {
            backgroundMusic = backgroundMusicObject.GetComponent<AudioSource>();
        }
        isMuted = PlayerPrefs.GetInt("Muted", 0) == 1;
        UpdateSoundState();

        soundButton.onClick.AddListener(ToggleSound);
    }

    void ToggleSound()
    {
        isMuted = !isMuted;
        PlayerPrefs.SetInt("Muted", isMuted ? 1 : 0);
        PlayerPrefs.Save();
        UpdateSoundState();
    }

    void UpdateSoundState()
    {
        backgroundMusic.mute = isMuted;
        soundButton.image.sprite = isMuted ? soundOffSprite : soundOnSprite;
    }
}
