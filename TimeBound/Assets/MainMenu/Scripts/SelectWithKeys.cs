using System.Collections;
using Unity.VisualScripting;
using System;
using UnityEngine;
using UnityEngine.Events;

public class SelectWithKeys : MonoBehaviour
{
    private bool isChanging = false;

    [Header("SelectType")]
    [SerializeField] bool horzNavigation;
    [SerializeField] bool vertNavigation;

    [Header("Adjustments")]
    [SerializeField] int colSize;
    public float delayEachChange = 0.5f;

    [Header("References")]
    [SerializeField] GameObject[] buttons;
    [SerializeField] KeyCode[] selectKeys = {KeyCode.Return,KeyCode.Space,KeyCode.JoystickButton0};
    public UnityEvent[] methods;

    private int currentButton = 0;

    private InputState inputScript;
    private ControlState prevState = ControlState.None;

    void Start()
    {
        inputScript = GetComponent<InputState>();
    }

    void Update()
    {
        if(InputState.currentState != prevState && InputState.currentState == ControlState.Mouse)
        {
            foreach(var button in buttons)
            {
                foreach (UnityEvent method in button.GetComponent<MouseHover>().OnLeave)
                {
                    method?.Invoke();
                }
            }
            currentButton = -1;
        }
        foreach (KeyCode key in selectKeys)
        {
            if (Input.GetKeyDown(key) && InputState.currentState != ControlState.Mouse)
            {
                if (currentButton == -1) { continue; }
                methods[currentButton]?.Invoke();
            }
        }
        if (InputState.currentState != prevState && InputState.currentState != ControlState.Mouse)
        {
            currentButton = 0;
            foreach (var button in buttons)
            {
                foreach (UnityEvent method in button.GetComponent<MouseHover>().OnLeave)
                {
                    method?.Invoke();
                }
            }
            foreach (UnityEvent method in buttons[currentButton].GetComponent<MouseHover>().OnEnter)
            {
                method?.Invoke();
            }
        }
        if (InputState.currentState != ControlState.Mouse && !isChanging)
        {
            changeButton();
        }
        if (Input.GetAxisRaw("Vertical") == 0 &&
            Input.GetAxisRaw("Horizontal") == 0)
        {
            isChanging = false;
            StopAllCoroutines();
        }


        prevState = InputState.currentState;
    }

    void changeButton()
    {
        if (vertNavigation)
        {
            if (Input.GetAxisRaw("Vertical") > 0)
            {
                isChanging = true;
                int currRow = currentButton / colSize;
                currentButton = currRow * colSize + (currentButton-1+buttons.Length*10)%colSize;
                //currentButton = (currentButton - 1 + buttons.Length * 10) % buttons.Length;
                foreach (var button in buttons)
                {
                    foreach(UnityEvent method in button.GetComponent<MouseHover>().OnLeave)
                    {
                        method?.Invoke();
                    }
                }
                foreach (UnityEvent method in buttons[currentButton].GetComponent<MouseHover>().OnEnter)
                {
                    method?.Invoke();
                }
                StartCoroutine(delay());
            }
            else if (Input.GetAxisRaw("Vertical") < 0)
            {
                isChanging = true;
                int currRow = currentButton / colSize;
                currentButton = currRow * colSize + (currentButton + 1) % colSize;
                //currentButton = (currentButton + 1) % buttons.Length;
                foreach (var button in buttons)
                {
                    foreach (UnityEvent method in button.GetComponent<MouseHover>().OnLeave)
                    {
                        method?.Invoke();
                    }
                }
                foreach (UnityEvent method in buttons[currentButton].GetComponent<MouseHover>().OnEnter)
                {
                    method?.Invoke();
                }
                StartCoroutine(delay());
            }
        }

        if (horzNavigation)
        {
            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                isChanging = true;
                currentButton = (currentButton + colSize) % buttons.Length;
                foreach (var button in buttons)
                {
                    foreach (UnityEvent method in button.GetComponent<MouseHover>().OnLeave)
                    {
                        method?.Invoke();
                    }
                }
                foreach (UnityEvent method in buttons[currentButton].GetComponent<MouseHover>().OnEnter)
                {
                    method?.Invoke();
                }
                StartCoroutine(delay());
            }
            else if (Input.GetAxisRaw("Horizontal") < 0)
            {
                isChanging = true;
                currentButton = (currentButton - colSize + buttons.Length * 10) % buttons.Length;
                foreach (var button in buttons)
                {
                    foreach (UnityEvent method in button.GetComponent<MouseHover>().OnLeave)
                    {
                        method?.Invoke();
                    }
                }
                foreach (UnityEvent method in buttons[currentButton].GetComponent<MouseHover>().OnEnter)
                {
                    method?.Invoke();
                }
                StartCoroutine(delay());
            }
        }
    }

    IEnumerator delay()
    {
        yield return new WaitForSeconds(delayEachChange);
        isChanging = false;
    }
}
