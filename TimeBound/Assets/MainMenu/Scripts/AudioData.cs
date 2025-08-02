using UnityEngine;

public class AudioData : MonoBehaviour
{
    public static AudioData Instance;

    [Range(0f, 1f)] public float musicVolume = 1f;
    [Range(0f, 1f)] public float sfxVolume = 1f;
    [Range(0f, 1f)] public float narratorVolume = 1f;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            QualitySettings.vSyncCount = 1;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
