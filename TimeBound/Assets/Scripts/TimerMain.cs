using System.Runtime.Versioning;
using UnityEngine;

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
    [SerializeField] KeyCode[] timeManipulateKey = { KeyCode.Q};
    
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

        if (input != 0)
        {
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
            timeSinceLastManipulate = 0;
        }
    }


    private void Update()
    {
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
                isManipulatingTime = !isManipulatingTime;
                if (isManipulatingTime)
                {
                    currTimeBackup = currTime;
                }
                else
                {
                    currTime = currTimeBackup;
                }
            }
        }

        //Test code (remove in final build)
        if (Input.GetKeyDown(KeyCode.R)){ 
            currTime = 0;
        }
        
    }
}
