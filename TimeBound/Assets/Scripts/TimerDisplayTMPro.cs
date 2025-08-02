using UnityEngine;
using TMPro;

public class TimerDisplayTMPro : MonoBehaviour
{
    [Header("References")]
    public TimerMain timerMain;  // Drag your TimerMain here
    public TextMeshProUGUI timerText;  // Drag the TMP Text component here

    void Reset()
    {
        // Auto-assign if left empty in Inspector
        if (timerMain == null)
            timerMain = FindObjectOfType<TimerMain>();
        if (timerText == null)
            timerText = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (timerMain == null || timerText == null)
            return;

        int h = timerMain.hoursElapsed;
        int m = timerMain.minutesElapsed;

        // Format as HH:MM with leading zeros
        timerText.text = $"{h:00}:{m:00}";
    }
}
