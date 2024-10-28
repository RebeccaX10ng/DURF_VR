using UnityEngine;

using UnityEngine;

public class RestrictMovement : MonoBehaviour
{
    private float initialX;
    private Vector3 lastPosition;

    void Start()
    {
        initialX = transform.position.x;
        lastPosition = transform.position;
    }

    void Update()
    {
        Vector3 currentPosition = transform.position;

        // 检查是否被选中（假设选中状态下 transform 在移动）
        if (currentPosition != lastPosition)
        {
            // 物体被选中时，不做限制
            lastPosition = currentPosition;
        }
        else
        {
            // 物体未被选中，恢复 X 轴到 initialX
            currentPosition.x = initialX;
            transform.position = currentPosition;
        }

        lastPosition = currentPosition;
    }
}
