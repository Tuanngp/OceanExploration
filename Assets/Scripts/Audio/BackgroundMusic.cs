using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    
    public static BackgroundMusic Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    void Start()
    {
        GetComponent<AudioSource>().Play();
    }
}
