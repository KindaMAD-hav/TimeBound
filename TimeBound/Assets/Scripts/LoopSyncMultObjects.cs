using System.Collections.Generic;
using UnityEngine;

public class LoopSyncMultObjects : MonoBehaviour
{
    [System.Serializable]
    public struct BoolVector2
    {
        public bool x;
        public bool y;

        public BoolVector2(bool x, bool y)
        {
            this.x = x;
            this.y = y;
        }

        public override string ToString()
        {
            return $"({x}, {y})";
        }
    }

    [Header("References")]
    [SerializeField] TimerMain timerScript;

    [Header("Adjustables")]
    [SerializeField] float timeQuantam = 1 / 24f;
    [SerializeField] float[] animationTime = {};
    [SerializeField] AnimationClip[] clips = {};

    //private variables
    private Collider[] colliders;
    private MeshRenderer meshRenderer;
    private float timeSinceLastChange;
    private Vector2[] enterAndExitTimes;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        colliders = GetComponentsInChildren<Collider>();
        GenerateRanges();
    }

    void GenerateRanges()
    {
        List<Vector2> ranges = new List<Vector2>();
        int totalMinutes = 24 * 60; // 1440 minutes
        int i = 0;

        int start = 0;
        while (start < totalMinutes)
        {
            int length = Mathf.RoundToInt(animationTime[i]);
            int end = Mathf.Min(start + length, totalMinutes);
            ranges.Add(new Vector2(start, end));

            if (end == totalMinutes)
                break;

            start = end; // instead of skipping, start exactly where we left off
            i = (i + 1) % clips.Length;
        }

        enterAndExitTimes = ranges.ToArray();
    }

    void Update()
    {
        timeSinceLastChange += Time.deltaTime;
        if (timeSinceLastChange>= timeQuantam)
        {
            timeSinceLastChange = 0;
        }
        else
        {
            return;
        }
        float currentMinutes = timerScript.currTime;

        for (int i = 0; i < enterAndExitTimes.Length; i++)
        {
            float startMinutes = enterAndExitTimes[i].x;
            float endMinutes = enterAndExitTimes[i].y;

            if (currentMinutes >= startMinutes && currentMinutes <= endMinutes)
            {
                PlayClipSynced(clips[i%4], startMinutes, endMinutes, currentMinutes);
                return;
            }
        }
    }

    void PlayClipSynced(AnimationClip clip, float startMinutes, float endMinutes, float currentMinutes)
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
