using UnityEngine;
public enum ControlState { None, Keyboard, Mouse }

public class InputState : MonoBehaviour
{
    public float mouseMovementThreshold = 0.1f;
    public float joystickDeadzone = 0.2f;

    static public ControlState currentState = ControlState.None;

    [SerializeField] GameObject[] controlIcons;

    void Update()
    {
        if (IsMouseInput())
            SetState(ControlState.Mouse);
        if (IsKeyboardInput())
            SetState(ControlState.Keyboard);
        setIcon();
    }

    void setIcon()
    {
        if(currentState == ControlState.Mouse)
        {
            controlIcons[0].SetActive(true);
            controlIcons[1].SetActive(false);
            controlIcons[2].SetActive(false);
        }
        else
        {
            controlIcons[0].SetActive(false);
            controlIcons[1].SetActive(false);
            controlIcons[2].SetActive(true);
        }
    }

    void SetState(ControlState newState)
    {
        if (newState == currentState)
            return;

        currentState = newState;

        switch (currentState)
        {
            case ControlState.Mouse:
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                break;

            case ControlState.Keyboard:
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                break;
        }
    }

    bool IsKeyboardInput()
    {
        return Input.anyKeyDown && !IsMouseButton();
    }

    bool IsMouseInput()
    {
        return Mathf.Abs(Input.GetAxis("Mouse X")) > mouseMovementThreshold ||
               Mathf.Abs(Input.GetAxis("Mouse Y")) > mouseMovementThreshold ||
               Mathf.Abs(Input.GetAxis("Mouse ScrollWheel")) > 0.05f ||
               IsMouseButton();
    }

    bool IsMouseButton()
    {
        return Input.GetMouseButtonDown(0) ||
               Input.GetMouseButtonDown(1) ||
               Input.GetMouseButtonDown(2);
    }
}
