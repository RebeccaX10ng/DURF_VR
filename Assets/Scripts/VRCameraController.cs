using UnityEngine;

public class MirrorCamera : MonoBehaviour
{
    public Camera mainCamera; 
    public Camera mirrorCamera; 
    void Update()
    {
        Vector3 mainCamPosition = mainCamera.transform.position;
        Vector3 eulerRotation = mainCamera.transform.rotation.eulerAngles;
        //eulerRotation.y = -eulerRotation.y;
        
        Vector3 mirrorCamPosition = new Vector3(-mainCamPosition.x - 1, mainCamPosition.y, mainCamPosition.z - 5);
        
        //Quaternion mirrorCamRotation = new Quaternion.Euler(eulerRotation);
        //Quaternion mirrorCamRotation = new Quaternion(0, -eulerRotation.y-180f, 0, 0);
        
        mirrorCamera.transform.position = mirrorCamPosition;
        //mirrorCamera.transform.rotation = mirrorCamRotation;
    }
}
