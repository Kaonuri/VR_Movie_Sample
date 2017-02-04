using System;
using UnityEngine;

public class AirVRInputHandler : MonoBehaviour
{
    public enum SwipeDirection
    {
        NONE,
        UP,
        DOWN,
        LEFT,
        RIGHT
    };

    public event Action<SwipeDirection> OnSwipe;                
    public event Action OnClick;                                
    public event Action OnDown;                                 
    public event Action OnUp;                                   
    public event Action OnDoubleClick;                          
    public event Action OnCancel;

    [SerializeField] AirVRCameraRig airVRCameraRig;
    [SerializeField] float doubleClickTime = 0.3f;    
    [SerializeField] float swipeWidth = 0.3f;
    [SerializeField] bool MouseInput = false;

    Vector2 touchDownPosition;                        
    Vector2 touchUpPosition;                          
    float lastMouseUpTime;                            
    float lastHorizontalValue;                        
    float lastVerticalValue;         
    
    public float DoubleClickTime { get { return doubleClickTime; } }

    void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        if(!airVRCameraRig.isBoundToClient && !MouseInput)
            return;

        SwipeDirection swipe = SwipeDirection.NONE;

        if (AirVRInput.GetButtonDown(airVRCameraRig, AirVRInput.Touchpad.Button.Touch) || Input.GetMouseButtonDown(0))
        {
            touchDownPosition = new Vector2(AirVRInput.GetAxis(airVRCameraRig, AirVRInput.Touchpad.Axis.PositionX), AirVRInput.GetAxis(airVRCameraRig, AirVRInput.Touchpad.Axis.PositionY));
            if (OnDown != null)
                OnDown();
        }
        
        if (AirVRInput.GetButtonUp(airVRCameraRig, AirVRInput.Touchpad.Button.Touch) || Input.GetMouseButtonUp(0))
        {
            touchUpPosition = new Vector2(AirVRInput.GetAxis(airVRCameraRig, AirVRInput.Touchpad.Axis.PositionX), AirVRInput.GetAxis(airVRCameraRig, AirVRInput.Touchpad.Axis.PositionY));

            swipe = DetectSwipe();

            if (OnSwipe != null)
                OnSwipe(swipe);

            if (OnUp != null)
                OnUp();

            if(Time.time - lastMouseUpTime < doubleClickTime)
            {
                if(OnDoubleClick != null)
                {
                    OnDoubleClick();
                }
            }

            else
            {
                if (OnClick != null)
                {
                    OnClick();
                }
            }

            lastMouseUpTime = Time.time;
        }

        if(AirVRInput.GetButtonDown(airVRCameraRig, AirVRInput.Touchpad.Button.BackButton))
        {
            if (OnCancel != null)
                OnCancel();
        }
    }

    SwipeDirection DetectSwipe()
    {
        Vector2 swipeData = (touchUpPosition - touchDownPosition).normalized;

        bool swipeIsVertical = Mathf.Abs(swipeData.x) < swipeWidth;
        bool swipeIsHorizontal = Mathf.Abs(swipeData.y) < swipeWidth;

        if (swipeData.y > 0f && swipeIsVertical)
            return SwipeDirection.UP;

        if (swipeData.y < 0f && swipeIsVertical)
            return SwipeDirection.DOWN;

        if (swipeData.x > 0f && swipeIsHorizontal)
            return SwipeDirection.RIGHT;

        if (swipeData.x < 0f && swipeIsHorizontal)
            return SwipeDirection.LEFT;

        return SwipeDirection.NONE;
    }

    void OnDestroy()
    {
        OnSwipe = null;
        OnClick = null;
        OnDoubleClick = null;
        OnDown = null;
        OnUp = null;
    }
}