using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRCameraController : MonoBehaviour
{
    public Camera mainCamera;
    public Camera vrCamera;

    void Start()
    {
        SyncVRCameraWithMainCamera();
    }

    void Update()
    {
        SyncVRCameraWithMainCamera();
    }

    private void SyncVRCameraWithMainCamera()
    {
        Vector3 mainCameraPosition = mainCamera.transform.position;
        Vector3 vrCameraPosition = new Vector3(mainCameraPosition.x, mainCameraPosition.y, mainCameraPosition.z - 5f);
        
        vrCamera.transform.position = vrCameraPosition;
        vrCamera.transform.rotation = mainCamera.transform.rotation;
    }
}
