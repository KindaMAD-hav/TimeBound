using UnityEngine;

public class DoorController : MonoBehaviour
{
    private bool isOpen = false;
    [Header("Audio")]
    public AudioClip doorOpenSound;
    [Range(0f, 1f)]
    public float doorVolume = 1f;
    public void OpenDoor()
    {
        if (isOpen) return;
        isOpen = true;

        // 🔊 Play the door opening sound
        if (doorOpenSound != null)
            AudioSource.PlayClipAtPoint(doorOpenSound, transform.position, doorVolume);

        // 🚪 Trigger the animation
        Animator anim = GetComponent<Animator>();
        if (anim != null)
            anim.SetTrigger("Open");

        // Optional: disable collider after open
        Collider col = GetComponent<Collider>();
        if (col != null)
            col.enabled = false;
    }

}
