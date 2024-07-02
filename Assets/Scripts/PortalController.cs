using UnityEngine;

public class PortalController : MonoBehaviour
{
    public Transform linkedPortal; // 链接的另一个传送门
    public Camera portalCamera; // 传送门相机
    public Transform playerCamera; // 玩家相机

    void Update()
    {
        if (linkedPortal == null || portalCamera == null || playerCamera == null)
        {
            return; // 确保所有引用都已设置
        }

        // 计算相对位置和方向
        Vector3 offset = playerCamera.position - linkedPortal.position;
        portalCamera.transform.position = transform.position + offset;

        // 计算相对旋转
        Quaternion relativeRotation = Quaternion.Inverse(linkedPortal.rotation) * playerCamera.rotation;
        portalCamera.transform.rotation = transform.rotation * relativeRotation;
    }
}