using UnityEngine;

public class PaintingAreaChecker : MonoBehaviour
{
    public GameObject targetObject; // 用于检测的目标物体
    public Material newMaterial; // 新的材质
    private Material originalMaterial; // 原来的材质
    private bool isTargetInside = false;
    private bool hasLoggedSuccess = false; // 状态变量，用于追踪日志消息是否已经发送

    public bool IsTargetInside
    {
        get { return isTargetInside; }
    }

    void Start()
    {
        if (targetObject != null)
        {
            MeshRenderer renderer = targetObject.GetComponent<MeshRenderer>();
            if (renderer != null)
            {
                originalMaterial = renderer.material;
            }
        }
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == targetObject)
        {
            // 检查目标物体是否完全在触发器范围内
            Collider targetCollider = targetObject.GetComponent<Collider>();
            Bounds targetBounds = targetCollider.bounds;
            Bounds triggerBounds = GetComponent<Collider>().bounds;

            bool wasTargetInside = isTargetInside;
            isTargetInside = triggerBounds.Contains(targetBounds.min) && triggerBounds.Contains(targetBounds.max);

            // 只在目标物体完全进入范围且之前未进入范围时发出日志消息
            if (isTargetInside && !wasTargetInside && !hasLoggedSuccess)
            {
                Debug.Log($"Success! {targetObject.name} is fully in the target area.");
                hasLoggedSuccess = true;
                
                MeshRenderer renderer = targetObject.GetComponent<MeshRenderer>();
                if (renderer != null && newMaterial != null)
                {
                    renderer.material = newMaterial;
                }
            }
            else if (!isTargetInside && wasTargetInside)
            {
                Debug.Log($"{targetObject.name} is partially outside the target area.");
                hasLoggedSuccess = false; // 重置日志状态
                
                MeshRenderer renderer = targetObject.GetComponent<MeshRenderer>();
                if (renderer != null && originalMaterial != null)
                {
                    renderer.material = originalMaterial;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == targetObject)
        {
            isTargetInside = false;
            hasLoggedSuccess = false; // 重置日志状态以便下一次进入时能够再次发出日志
            
            MeshRenderer renderer = targetObject.GetComponent<MeshRenderer>();
            if (renderer != null && originalMaterial != null)
            {
                renderer.material = originalMaterial;
            }
        }
    }
}