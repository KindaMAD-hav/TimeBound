using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class NarrationSequence : MonoBehaviour
{
    [Header("Narration Clips")]
    public AudioClip[] narrationClips;
    public float delayBetweenClips = 0.5f;

    private AudioSource audioSource;
    private bool hasPlayed = false;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 0f;
        audioSource.volume = 1f;
    }

    public void PlaySequence()
    {
        if (!hasPlayed && narrationClips.Length > 0)
        {
            Debug.Log("Playing narration sequence...");
            StartCoroutine(PlayClips());
        }
    }

    private IEnumerator PlayClips()
    {
        hasPlayed = true;

        foreach (var clip in narrationClips)
        {
            audioSource.clip = clip;
            audioSource.Play();
            yield return new WaitForSeconds(clip.length + delayBetweenClips);
        }
    }
}
