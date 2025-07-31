using UnityEngine;

public class SyncObjects : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TimerMain timerScript;

    [Header("Adjustables")]
    [SerializeField] Vector2[] enterAndExitTimes; // start & end times in minutes
    [SerializeField] AnimationClip[] clips;

    void Update()
    {
        int currentMinutes = Mathf.FloorToInt(timerScript.currTime);

        for (int i = 0; i < enterAndExitTimes.Length; i++)
        {
            int startMinutes = (int)enterAndExitTimes[i].x;
            int endMinutes = (int)enterAndExitTimes[i].y;

            if (currentMinutes >= startMinutes && currentMinutes <= endMinutes)
            {
                PlayClipSynced(clips[i], startMinutes, endMinutes, currentMinutes);
                return; 
            }
        }
    }

    void PlayClipSynced(AnimationClip clip, int startMinutes, int endMinutes, int currentMinutes)
    {
        if (clip == null) return;

        // Progress in current time range (0–1)
        float t = Mathf.InverseLerp(startMinutes, endMinutes, currentMinutes);

        // Map progress to animation time in seconds
        float clipTime = t * clip.length;

        // Directly set animation pose without Animator
        clip.SampleAnimation(gameObject, clipTime);
    }
}
