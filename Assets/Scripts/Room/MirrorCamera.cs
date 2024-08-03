using UnityEngine;

public class MirrorCamera : MonoBehaviour
{
    public Transform targetCamera;  // 目标相机的Transform
    public Vector3 positionOffset;  // 位置偏移
    public Vector3 rotationOffset;  // 旋转偏移

    private void LateUpdate()
    {
        if (targetCamera != null)
        {
            // 更新位置
            transform.position = targetCamera.position + positionOffset;
        }
    }
}
