using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SettingsKeyboardNav : MonoBehaviour
{
    [Header("Navigation")]
    public Selectable firstSelectable;
    private ControlState lastState = ControlState.None;
    private GameObject lastHoveredObject;

    [Header("Audio")]
    public AudioSource uiAudioSource;
    public AudioClip hoverSound;

    void Update()
    {
        // --- Detect state change ---
        if (InputState.currentState != lastState)
        {
            EventSystem.current.SetSelectedGameObject(null);
            Canvas.ForceUpdateCanvases();

            if (InputState.currentState == ControlState.Keyboard && firstSelectable != null)
            {
                EventSystem.current.SetSelectedGameObject(firstSelectable.gameObject);
                PlayHoverSound();
                lastHoveredObject = firstSelectable.gameObject;
            }

            lastState = InputState.currentState;
        }

        // --- Keyboard hover detection ---
        if (InputState.currentState == ControlState.Keyboard)
        {
            var currentSelected = EventSystem.current.currentSelectedGameObject;
            if (currentSelected != null && currentSelected != lastHoveredObject)
            {
                PlayHoverSound();
                lastHoveredObject = currentSelected;
            }

            // Spacebar acts like Enter
            if (currentSelected != null && Input.GetKeyDown(KeyCode.Space))
            {
                ExecuteEvents.Execute(currentSelected, new BaseEventData(EventSystem.current), ExecuteEvents.submitHandler);
            }

            // Keep dropdown option visible while navigating
            KeepSelectedVisible(currentSelected);
        }
    }

    private void KeepSelectedVisible(GameObject currentSelected)
    {
        if (currentSelected == null) return;

        if (currentSelected.transform.name == "Item Background" ||
            currentSelected.transform.name == "Item Label" ||
            currentSelected.transform.name.StartsWith("Item"))
        {
            ScrollRect scrollRect = currentSelected.GetComponentInParent<ScrollRect>();
            if (scrollRect == null) return;

            RectTransform selectedRect = currentSelected.GetComponent<RectTransform>();
            if (selectedRect == null) return;

            Canvas.ForceUpdateCanvases();

            RectTransform viewport = scrollRect.viewport;
            if (viewport == null) viewport = scrollRect.GetComponent<RectTransform>();

            Vector3[] itemCorners = new Vector3[4];
            selectedRect.GetWorldCorners(itemCorners);
            Vector3[] viewportCorners = new Vector3[4];
            viewport.GetWorldCorners(viewportCorners);

            float contentHeight = scrollRect.content.rect.height - viewport.rect.height;
            float itemPos = Mathf.Abs(scrollRect.content.InverseTransformPoint(selectedRect.position).y);

            // Added offset to ensure top items snap fully to top
            const float topOffset = 5f; // tweak this value if needed
            const float bottomOffset = 5f;

            bool above = itemCorners[1].y > viewportCorners[1].y + topOffset;
            bool below = itemCorners[0].y < viewportCorners[0].y - bottomOffset;

            if (above || below)
            {
                float normalizedPos = Mathf.Clamp01(1 - ((itemPos - topOffset) / contentHeight));
                scrollRect.verticalNormalizedPosition = normalizedPos;
            }
        }
    }



    public void PlayHoverSound()
    {
        if (uiAudioSource != null && hoverSound != null)
            uiAudioSource.PlayOneShot(hoverSound);
    }
}
