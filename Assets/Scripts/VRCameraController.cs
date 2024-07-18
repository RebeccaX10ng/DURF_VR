using UnityEngine;

public class MirrorCamera : MonoBehaviour
{
    public Camera mainCamera; 
    public Camera mirrorCamera; 
    void Update()
    {
        Vector3 mainCamPosition = mainCamera.transform.position;
        
        Vector3 mirrorCamPosition = new Vector3(-mainCamPosition.x - 1, mainCamPosition.y, mainCamPosition.z - 5);
        
        mirrorCamera.transform.position = mirrorCamPosition;
    }
}
