using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour
{
    public AudioSource audioSource;
    public Button button;

    void Start()
    {     
        button.onClick.AddListener(PlaySound);
    }
   
    void PlaySound()
    {
        audioSource.Play();
    }
}
