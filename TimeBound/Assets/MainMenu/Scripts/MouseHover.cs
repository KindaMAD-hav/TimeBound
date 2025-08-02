using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MouseHover : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    [Header("References")]
    public UnityEvent[] OnEnter;
    public UnityEvent[] OnLeave;

    private Outline outline;
    Vector3 startPos;

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        foreach(UnityEvent method in OnEnter)
        {
            method?.Invoke();
        }
    }
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        foreach (UnityEvent method in OnLeave)
        {
            method?.Invoke();
        }
    }

    

}
