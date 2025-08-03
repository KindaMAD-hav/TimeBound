using System.Runtime.Versioning;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class TimerMain : MonoBehaviour
{
    [Header("Data")]
    public float currTime = 0;
    public int hoursElapsed = 0;
    public int minutesElapsed = 0;
    public bool isManipulatingTime = false;

    [Header("Adjustments")]
    [SerializeField] float updateQuantum = 1/24; // After how much time do we update the time
    [SerializeField] float timeManipulateSenstivity = 10;

    [Header("References")]
    [SerializeField] KeyCode[] timeManipulateKey = { KeyCode.LeftControl};
    [SerializeField] Image overlay;
    [SerializeField] AudioClip rewindClip;
    [SerializeField] AudioSource source;
    [SerializeField] Canvas canvas;

    //private variables
    private float timeSinceLastUpdate = 0;
    private float timeSinceLastManipulate = 0;
    private float currTimeBackup = 0;


    public void resetTimer()
    {
        currTime = 0;
    }

    private void manipulateTime()
    {
        float input = Input.GetAxisRaw("Horizontal");
        source.clip = rewindClip;

        if (input != 0)
        {
            if (!source.isPlaying)
            {
                source.Play();
            }
            timeSinceLastManipulate += Time.deltaTime * timeManipulateSenstivity/updateQuantum;

            if (timeSinceLastManipulate >= 1f)
            {
                int steps = Mathf.FloorToInt(timeSinceLastManipulate);
                timeSinceLastManipulate -= steps;

                currTime += steps * updateQuantum * Mathf.Sign(input);
            }
        }
        else
        {
            if (source.isPlaying)
            {
                source.Stop();
            }
            timeSinceLastManipulate = 0;
        }
    }


    private void Update()
    {
        if(canvas.enabled == true)
        {
            overlay.enabled = false;
            if(source.isPlaying && source.clip == rewindClip) source.Stop();
            return;
        }
        if (!isManipulatingTime)
        {
            timeSinceLastUpdate += Time.deltaTime;
            if (timeSinceLastUpdate >= updateQuantum)
            {
                timeSinceLastUpdate -= updateQuantum;
                currTime += updateQuantum;
            }
        }
        else
        {
            manipulateTime();
        }
        currTime = Mathf.Clamp(currTime, 0, 24 * 60);
        hoursElapsed = (int)currTime / 60;
        minutesElapsed = (int)currTime % 60;

        foreach (KeyCode key in timeManipulateKey)
        {
            if (Input.GetKeyDown(key))
            {
                isManipulatingTime = true;
                currTimeBackup = currTime;
                overlay.enabled = true;
            }else if(Input.GetKeyUp(key))
            {
                isManipulatingTime = false;
                currTime = currTimeBackup;
                overlay.enabled = false;
            }

            if (!Input.GetKey(key)) // AdditionalSafety
            {
                isManipulatingTime = false;
                overlay.enabled = false;
                if (source.isPlaying)
                {
                    source.Stop();
                }
            }
            else
            {
                isManipulatingTime = true;
                overlay.enabled = true;
            }
        }

       
        
    }
}
