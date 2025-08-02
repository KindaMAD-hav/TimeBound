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

                // Optional pickup sound
                var po = obj.GetComponent<PickupableObject>();
                if (po != null && po.pickupSound != null)
                    AudioSource.PlayClipAtPoint(po.pickupSound, obj.transform.position, po.pickupVolume);

                // Start narration sequence (if present)
                var narration = obj.GetComponent<PickupNarrationPlayer>();
                if (narration != null)
                    narration.StartNarration();

                // Deactivate visuals, but leave audio working
                foreach (var renderer in obj.GetComponentsInChildren<Renderer>())
                    renderer.enabled = false;
                foreach (var collider in obj.GetComponentsInChildren<Collider>())
                    collider.enabled = false;

                inventory.Add(obj);

                Debug.Log($"Picked up: {obj.name} (Total items: {inventory.Count})");
            }
        }
    }


    // (Optional) Expose inventory to other systems:
    public IReadOnlyList<GameObject> Inventory => inventory;
}
