using System.Collections.Generic;
using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    [Header("Setup")]
    public Camera playerCamera;
    public float pickupRange = 3f;

    // Your “inventory” just a list of picked-up GameObjects
    private List<GameObject> inventory = new List<GameObject>();

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            TryPickup();
    }

    void TryPickup()
    {
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, pickupRange))
        {
            if (hit.collider.CompareTag("Pickupable"))
            {
                GameObject obj = hit.collider.gameObject;

                // ▶︎ Play pickup sound (optional)
                var po = obj.GetComponent<PickupableObject>();
                if (po != null && po.pickupSound != null)
                {
                    AudioSource.PlayClipAtPoint(po.pickupSound, obj.transform.position, po.pickupVolume);
                }

                // ▶︎ If this object has a narration sequence, play it
                var narration = obj.GetComponent<NarrationSequence>();
                if (narration != null)
                {
                    // We play the sequence before deactivating the object
                    narration.PlaySequence();
                }

                inventory.Add(obj);

                // Option 1: hide the object and let narration play
                obj.SetActive(false);

                // Option 2: delay disabling until narration finishes
                // StartCoroutine(DisableAfterNarration(obj, narration));

                Debug.Log($"Picked up: {obj.name} (Total items: {inventory.Count})");
            }
        }
    }

    // (Optional) Expose inventory to other systems:
    public IReadOnlyList<GameObject> Inventory => inventory;
}
