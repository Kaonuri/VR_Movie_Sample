using UnityEngine;
using UnityEngine.UI;

public class Reticle : MonoBehaviour
{
    [SerializeField] float defaultDistnace = 5f;     
    [SerializeField] bool useNormal;
    [SerializeField] Image reticleImage;
    [SerializeField] Transform reticleTransform;
    [SerializeField] Transform centerEyeAnchor;

    Vector3 originalScale;                            
    Quaternion originalRotation;                      


    public bool UseNormal
    {
        get { return useNormal; }
        set { useNormal = value; }
    }


    public Transform ReticleTransform { get { return reticleTransform; } }


    void Awake()
    {        
        originalScale = reticleTransform.localScale;
        originalRotation = reticleTransform.localRotation;
    }


    public void Hide()
    {
        reticleImage.enabled = false;
    }


    public void Show()
    {
        reticleImage.enabled = true;
    }


    public void SetPosition()
    {
        reticleTransform.position = centerEyeAnchor.position + centerEyeAnchor.forward * defaultDistnace;
        reticleTransform.localScale = originalScale * defaultDistnace;
        reticleTransform.localRotation = originalRotation;
    }


    public void SetPosition(RaycastHit hit)
    {
        reticleTransform.position = hit.point;
        reticleTransform.localScale = originalScale * hit.distance;

        if (useNormal)
            reticleTransform.rotation = Quaternion.FromToRotation(Vector3.forward, hit.normal);
        else
            reticleTransform.localRotation = originalRotation;
    }
}
