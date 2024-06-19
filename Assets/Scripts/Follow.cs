using UnityEngine;

public class Follow : MonoBehaviour
{
    public GameObject[] trackedObjects; // 要跟踪的多个物体
    private Vector3[] initialPositionOffsets; // 每个物体的初始位置偏移量

    private void Start()
    {
        // 初始化位置偏移量数组
        initialPositionOffsets = new Vector3[trackedObjects.Length];

        // 计算每个物体的初始位置偏移量
        for (int i = 0; i < trackedObjects.Length; i++)
        {
            Vector3 offset = trackedObjects[i].transform.position - Camera.main.transform.position;
            initialPositionOffsets[i] = offset;
        }

        // 移动物体到初始位置
        MoveObjectsToInitialPosition();
    }

    // 将物体移动到初始位置的方法
    private void MoveObjectsToInitialPosition()
    {
        for (int i = 0; i < trackedObjects.Length; i++)
        {
            trackedObjects[i].transform.position = Camera.main.transform.position + initialPositionOffsets[i];
        }
    }
}