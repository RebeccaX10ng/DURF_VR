using UnityEngine;

public class PaintingAreaChecker : MonoBehaviour
{
    public GameObject[] objects; // 对象数组
    public Collider[] targetColliders; // 目标区域数组

    private bool[] objectsInTarget;

    void Start()
    {
        if (objects.Length != targetColliders.Length)
        {
            Debug.LogError("Objects and targetColliders arrays must have the same length!");
            return;
        }

        objectsInTarget = new bool[objects.Length];
    }

    void Update()
    {
        CheckAllObjectsInTargets();
    }

    private void CheckAllObjectsInTargets()
    {
        bool allInTarget = true;

        for (int i = 0; i < objects.Length; i++)
        {
            if (!IsObjectFullyInTarget(i))
            {
                allInTarget = false;
                // 添加调试信息
                Debug.Log($"{objects[i].name} is not fully in target area.");
            }
        }

        if (allInTarget)
        {
            Debug.Log("Success! All objects are fully in their target areas.");
        }
    }

    private bool IsObjectFullyInTarget(int index)
    {
        Collider objectCollider = objects[index].GetComponent<Collider>();
        Bounds objectBounds = objectCollider.bounds;
        Bounds targetBounds = targetColliders[index].bounds;

        return targetBounds.Contains(objectBounds.min) && targetBounds.Contains(objectBounds.max);
    }
}