using UnityEngine;

public class MainMenuSelectButton : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject ManualOutline;
    [SerializeField] AudioSource AudioSource;
    [SerializeField] AudioClip navigationSound;
    [SerializeField] GameObject[] elementsToOffset;

    [Header("Adjustments")]
    [SerializeField] float xOffset = 5;
    [SerializeField] float yOffset = -1;
    [SerializeField] Vector3[] additionalOffsets;

    Vector3 startPos;

    void Start()
    {
        if (ManualOutline != null)
        {
            ManualOutline.SetActive(false);
        }
        startPos = transform.position;
        xOffset = xOffset * Screen.width / 1920;
        yOffset = yOffset * Screen.height / 1080;
        for (int i = 0; i < additionalOffsets.Length; i++)
        {
            additionalOffsets[i] = new Vector3(additionalOffsets[i].x * Screen.width / 1920, additionalOffsets[i].y * Screen.height / 1080, 0);
        }
    }

    public void ChangeState(bool enabled)
    {
        Vector3 Pos = enabled ? startPos + new Vector3(xOffset, yOffset, 0) : startPos;
        if (ManualOutline != null)
        {
            ManualOutline.SetActive(enabled);
        }
        for (int i = 0; i < additionalOffsets.Length; i++)
        {
            elementsToOffset[i].transform.position = Pos + additionalOffsets[i];
        }
        if (navigationSound != null)
        {
            if (enabled == true) AudioSource.PlayOneShot(navigationSound);
        }
    }
}
