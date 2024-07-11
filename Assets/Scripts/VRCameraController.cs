using UnityEngine;

public class MirrorCamera : MonoBehaviour
{
    public Camera mainCamera; // 主相机
    public Camera mirrorCamera; // 镜像相机

    void Update()
    {
        // 获取主相机的位置和旋转
        Vector3 mainCamPosition = mainCamera.transform.position;
        Quaternion mainCamRotation = mainCamera.transform.rotation;

        // 计算镜像相机的位置
        Vector3 mirrorCamPosition = new Vector3(-mainCamPosition.x - 1, mainCamPosition.y, mainCamPosition.z);

        // 计算镜像相机的旋转
        Quaternion mirrorCamRotation = new Quaternion(-mainCamRotation.x, mainCamRotation.y, mainCamRotation.z, -mainCamRotation.w);

        // 应用到镜像相机
        mirrorCamera.transform.position = mirrorCamPosition;
        mirrorCamera.transform.rotation = mirrorCamRotation;
    }
}
