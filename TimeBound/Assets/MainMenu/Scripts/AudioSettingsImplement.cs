using UnityEngine;

public class AudioSettingsImplement : MonoBehaviour
{
    private AudioSource musicSource;
    [SerializeField] AudioSource[] narratorSources = {};
    [SerializeField] AudioSource[] sfxSources = {};

    private void Start()
    {
        musicSource = BGmusic.instance.GetComponent<AudioSource>();
    }

    private void Update()
    {
        musicSource.volume =  AudioData.Instance.musicVolume;
        foreach (AudioSource sfxSource in sfxSources)
        {
            sfxSource.volume = AudioData.Instance.sfxVolume;
        }
    }
}
