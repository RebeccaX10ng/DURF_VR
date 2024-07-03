using UnityEngine;

public class RotateWithSphere : MonoBehaviour
{
    [SerializeField]
    XRSphere rotatingSphere;

    [SerializeField]
    Transform[] objectsToRotate;

    void OnEnable()
    {
        if (rotatingSphere != null)
        {
            rotatingSphere.onRotationChange.AddListener(OnRotationChange);
        }
    }

    void OnDisable()
    {
        if (rotatingSphere != null)
        {
            rotatingSphere.onRotationChange.RemoveListener(OnRotationChange);
        }
    }

    void OnRotationChange(Quaternion newRotation)
    {
        foreach (Transform obj in objectsToRotate)
        {
            obj.rotation = newRotation;
        }
    }
}