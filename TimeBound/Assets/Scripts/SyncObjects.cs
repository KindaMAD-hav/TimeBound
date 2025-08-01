using UnityEngine;

public class SyncObjects : MonoBehaviour
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
    [SerializeField] float timeQuantam = 1/24f;
    [SerializeField] Vector2[] enterAndExitTimes; // start & end times in minutes
    [SerializeField] AnimationClip[] clips;
    [SerializeField] bool enabledAtStart;
    [SerializeField] BoolVector2[] isEnabled;

    //private variables
    private Collider[] colliders;
    private MeshRenderer meshRenderer;
    private float timeSinceLastChange;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        colliders = GetComponentsInChildren<Collider>();
        setEnabledState(enabledAtStart);
    }

    private void setEnabledState(bool enabled)
    {
        meshRenderer.enabled = enabled;
        foreach (Collider collider in colliders)
        {
            collider.enabled = enabled;
        }
    }

    void Update()
    {
        if((enterAndExitTimes.Length != clips.Length) || (isEnabled.Length != clips.Length))
        {
            Debug.Log("Adjustable arrays do not have the same length!");
            return;
        }
        timeSinceLastChange += Time.deltaTime;
        if (timeSinceLastChange >= timeQuantam)
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

            if(currentMinutes >=startMinutes && currentMinutes <= endMinutes)
            {
                Debug.Log("Inside time");
                setEnabledState(isEnabled[i].x);
            }
            else if (i!=enterAndExitTimes.Length-1 && currentMinutes > endMinutes && currentMinutes <= enterAndExitTimes[i + 1].y)
            {
                setEnabledState(isEnabled[i].y);
                Debug.Log("Outside time");
            }
            else if (currentMinutes > enterAndExitTimes[enterAndExitTimes.Length - 1].y)
            {
                setEnabledState(isEnabled[i].y);
                Debug.Log("End Reached");
            }
            else if(currentMinutes < enterAndExitTimes[0].x)
            {
                setEnabledState(isEnabled[i].y);
                Debug.Log("Start Reached");
            }

            if (currentMinutes >= startMinutes && currentMinutes <= endMinutes)
            {
                PlayClipSynced(clips[i], startMinutes, endMinutes, currentMinutes);
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
