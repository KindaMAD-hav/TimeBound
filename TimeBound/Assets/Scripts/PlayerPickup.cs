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
            // Only pick up objects tagged "Pickupable"
            if (hit.collider.CompareTag("Pickupable"))
            {
                GameObject obj = hit.collider.gameObject;
                inventory.Add(obj);

                // Remove it from the scene
                obj.SetActive(false);
                // —or— Destroy(obj);

                Debug.Log($"Picked up: {obj.name} (Total items: {inventory.Count})");
            }
        }
    }

    // (Optional) Expose inventory to other systems:
    public IReadOnlyList<GameObject> Inventory => inventory;
}
