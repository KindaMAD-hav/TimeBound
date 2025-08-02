using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class PickupableObject : MonoBehaviour
{
    [Header("Pickup Sound")]
    [Tooltip("The sound to play when this object is picked up")]
    public AudioClip pickupSound;

    [Range(0f, 1f)]
    public float pickupVolume = 1f;
}
