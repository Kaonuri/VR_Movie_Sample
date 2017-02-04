using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class AirVRInteractiveItem : MonoBehaviour
{
    public UnityEvent onOver;
    public UnityEvent onOut;
    public UnityEvent onClick;
    public UnityEvent onDoubleClick;
    public UnityEvent onUp;
    public UnityEvent onDown;

    protected bool isOver;    

    public bool IsOver
    {
        get { return IsOver; }
    }

    public void Over()
    { 
        isOver = true;
        if(onOver != null)
            onOver.Invoke();
    }

    public void Out()
    {
        isOver = false;
        if (onOut != null)
            onOut.Invoke();
    }

    public void Click()
    {
        isOver = true;
        if (onClick != null)
            onClick.Invoke();
    }

    public void DoubleClick()
    {
        if (onDoubleClick != null)
            onDoubleClick.Invoke();
    }

    public void Up()
    {
        if (onUp != null)
            onUp.Invoke();
    }

    public void Down()
    {
        if (onDown != null)
            onDown.Invoke();
    }
}
