using System;
using UnityEngine;

public class AirVREyeRaycaster : MonoBehaviour
{
    public event Action<RaycastHit> OnRaycasthit;

    [SerializeField] Transform centerEyeAnchor;
    [SerializeField] LayerMask exclusionLayers;
    [SerializeField] Reticle reticle;
    [SerializeField] AirVRInputHandler airVRInputHandler;
    [SerializeField] bool showHitLog;
    [SerializeField] bool showDebugRay;
    [SerializeField] Color debugRayColor = Color.blue;
    [SerializeField] float debugRayLength = 5f;
    [SerializeField] float debugRayDuration = 0.1f;
    [SerializeField] float rayLength = 500f;

    AirVRInteractiveItem currentInteractible;                
    AirVRInteractiveItem lastInteractible;

    public AirVRInteractiveItem CurrentInteractible
    {
        get { return currentInteractible; }
    }

    public AirVRInteractiveItem LastInteractible
    {
        get { return lastInteractible; }
    }

    private void OnEnable()
    {
        airVRInputHandler.OnClick += HandleClick;
        airVRInputHandler.OnDoubleClick += HandleDoubleClick;
        airVRInputHandler.OnUp += HandleUp;
        airVRInputHandler.OnDown += HandleDown;
    }


    private void OnDisable()
    {
        airVRInputHandler.OnClick -= HandleClick;
        airVRInputHandler.OnDoubleClick -= HandleDoubleClick;
        airVRInputHandler.OnUp -= HandleUp;
        airVRInputHandler.OnDown -= HandleDown;
    }


    private void Update()
    {
        EyeRaycast();
    }


    private void EyeRaycast()
    {
        if (showDebugRay)
        {
            Debug.DrawRay(centerEyeAnchor.position, centerEyeAnchor.forward * debugRayLength, debugRayColor, debugRayDuration);
        }

        Ray ray = new Ray(centerEyeAnchor.position, centerEyeAnchor.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayLength, ~exclusionLayers))
        {
            if(showHitLog)
                Debug.Log(hit.transform.name);

            AirVRInteractiveItem interactible = hit.collider.GetComponent<AirVRInteractiveItem>();
            currentInteractible = interactible;

            if (interactible && interactible != lastInteractible)
                interactible.Over();

            if (interactible != lastInteractible)
                DeactiveLastInteractible();

            lastInteractible = interactible;

            if (reticle)
                reticle.SetPosition(hit);

            if (OnRaycasthit != null)
                OnRaycasthit(hit);

        }
        else
        {
            currentInteractible = null;
            DeactiveLastInteractible();

            if (reticle)
                reticle.SetPosition();
        }
    }


    private void DeactiveLastInteractible()
    {
        if (lastInteractible == null)
            return;

        lastInteractible.Out();
        lastInteractible = null;
    }


    private void HandleUp()
    {
        if (currentInteractible != null)
            currentInteractible.Up();
    }


    private void HandleDown()
    {
        if (currentInteractible != null)
            currentInteractible.Down();
    }


    private void HandleClick()
    {
        if (currentInteractible != null)
            currentInteractible.Click();
    }


    private void HandleDoubleClick()
    {
        if (currentInteractible != null)
            currentInteractible.DoubleClick();

    }
}