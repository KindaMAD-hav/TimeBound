using UnityEngine;

[RequireComponent(typeof(Collider))]
public class NarrationTrigger : MonoBehaviour
{
    [Header("Audio Settings")]
    [Tooltip("The clip to play when the player enters this trigger")]
    public AudioClip narrationClip;

    [Tooltip("Should this trigger only fire once?")]
    public bool playOnce = true;

    [Tooltip("Tag your player GameObject with this (default: “Player”)")]
    public string playerTag = "Player";

    // (Optional) adjust these if you want 2D UI sound or 3D world‐space sound
    [Range(0, 1)]
    public float spatialBlend = 0f;     // 0 = 2D, 1 = fully 3D
    public float volume = 1f;

    AudioSource _audioSource;
    bool _hasPlayed;

    void Awake()
    {
        // ensure we have a trigger collider
        var col = GetComponent<Collider>();
        col.isTrigger = true;

        // add & configure AudioSource
        _audioSource = gameObject.AddComponent<AudioSource>();
        _audioSource.playOnAwake = false;
        _audioSource.spatialBlend = spatialBlend;
        _audioSource.volume = volume;
    }

    void OnTriggerEnter(Collider other)
    {
        if (_hasPlayed && playOnce) return;
        if (!other.CompareTag(playerTag)) return;
        if (narrationClip == null) return;

        _audioSource.PlayOneShot(narrationClip);
        if (playOnce) _hasPlayed = true;
    }
}
