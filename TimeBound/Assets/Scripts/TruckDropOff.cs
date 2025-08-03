using System;
using UnityEngine;

public class TruckDropOff : MonoBehaviour
{
    [SerializeField] Transform DropOff;
    [SerializeField] TimerMain timerMain;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            print("hello");
            collision.gameObject.transform.position = DropOff.position;
            timerMain.currTime += 60;
            enabled = false;
        }
    }
}
