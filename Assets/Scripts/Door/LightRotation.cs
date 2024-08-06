using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRotation : MonoBehaviour
{
    public List<GameObject> rotationObjects;
    public Light directionalLight;
    public float ratio = 3f;
    private Dictionary<GameObject, Vector3> previousRotations = new Dictionary<GameObject, Vector3>();
    
    void Start()
    {
        foreach (var obj in rotationObjects)
        {
            if (obj != null)
            {
                previousRotations[obj] = obj.transform.eulerAngles;
            }
        }
    }

    void Update()
    {
        foreach (var obj in rotationObjects)
        {
            if (obj != null)
            {
                Vector3 currentRotation = obj.transform.eulerAngles;
                Vector3 previousRotation = previousRotations[obj];
                
                // Calculate the delta rotation for both x and y axes
                float deltaX = Mathf.DeltaAngle(previousRotation.x, currentRotation.x);
                float deltaY = Mathf.DeltaAngle(previousRotation.y, currentRotation.y);
                
                // Apply the delta rotations to the directional light's x-axis rotation only
                float totalDeltaX = (deltaX + deltaY) / ratio;
                Vector3 lightRotation = directionalLight.transform.rotation.eulerAngles;
                lightRotation.x += totalDeltaX;
                
                // Apply the new rotation to the directional light, preserving y and z axes
                directionalLight.transform.rotation = Quaternion.Euler(lightRotation.x, lightRotation.y, lightRotation.z);

                // Update the previous rotation
                previousRotations[obj] = currentRotation;
            }
        }
    }
}