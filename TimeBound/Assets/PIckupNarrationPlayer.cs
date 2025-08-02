public class PickupNarrationPlayer : MonoBehaviour
{
    [Header("Narration Settings")]
    public AudioClip[] narrationClips;
    public float delayBetweenClips = 0.5f;

    [Header("Door to Open (Optional)")]
    public DoorController doorToOpen;

    private AudioSource narrationSource;
    private int currentClipIndex = 0;
    private float nextClipTime = 0f;
    private bool isPlaying = false;

    private void Start()
    {
        narrationSource = gameObject.AddComponent<AudioSource>();
        narrationSource.playOnAwake = false;
        narrationSource.spatialBlend = 0f;
        narrationSource.volume = 1f;
    }

    public void StartNarration()
    {
        if (narrationClips.Length == 0 || isPlaying) return;

        isPlaying = true;
        currentClipIndex = 0;
        nextClipTime = Time.time;
    }

    private void Update()
    {
        if (!isPlaying || narrationClips.Length == 0)
            return;

        if (!narrationSource.isPlaying && Time.time >= nextClipTime)
        {
            if (currentClipIndex < narrationClips.Length)
            {
                narrationSource.clip = narrationClips[currentClipIndex];
                narrationSource.Play();
                nextClipTime = Time.time + narrationSource.clip.length + delayBetweenClips;
                currentClipIndex++;
            }
            else
            {
                isPlaying = false;

                // ▶ Notify the door after narration ends
                if (doorToOpen != null)
                {
                    doorToOpen.OpenDoor();
                }
            }
        }
    }
}
