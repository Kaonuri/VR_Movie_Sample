using UnityEngine;

public class MouseCamera : MonoBehaviour
{
    [SerializeField] Transform cameraRigTransform;
    [SerializeField] public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
    [SerializeField] public RotationAxes axes = RotationAxes.MouseXAndY;
    [SerializeField] float lerpSpeed = 10f;
    [SerializeField] float sensitivityX = 10f;
    [SerializeField] float sensitivityY = 10f;
    [SerializeField] float minimumX = -360f;
    [SerializeField] float maximumX = 360f;
    [SerializeField] float minimumY = -60f;
    [SerializeField] float maximumY = 60f;

    float rotationY = 0f;
    float rotationX = 0f;
    Vector3 targetRot;
#if UNITY_EDITOR
    void Update()
    {        
        switch(axes)
        {
            case RotationAxes.MouseXAndY:
                {
                    SetRotationX();
                    SetRotationY();
                }
                break;
            case RotationAxes.MouseX:
                {
                    SetRotationX();
                }
                break;
            case RotationAxes.MouseY:
                {
                    SetRotationY();
                }
                break;
        }

        Rotate();
    }
#endif

    void SetRotationX()
    {
        rotationX += Input.GetAxis("Mouse X") * sensitivityX;
        rotationX = Mathf.Clamp(rotationX, minimumX, maximumX);
    }

    void SetRotationY()
    {
        rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
        rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);
    }

    void Rotate()
    {
        targetRot = new Vector3(-rotationY, rotationX, 0f);
        cameraRigTransform.rotation = Quaternion.Slerp(cameraRigTransform.rotation, Quaternion.Euler(targetRot), Time.deltaTime * lerpSpeed);
    }
}

