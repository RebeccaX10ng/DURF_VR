using UnityEngine;

public class RestrictMovement : MonoBehaviour
{
    // 保存初始X轴的位置
    private float initialX;

    void Start()
    {
        // 获取物体初始位置的X值
        initialX = transform.position.x;
    }

    void Update()
    {
        // 获取物体当前的位置
        Vector3 currentPosition = transform.position;

        // 保持X轴位置不变，只允许在Y和Z轴上移动
        currentPosition.x = initialX;

        // 将限制后的位置应用到物体上
        transform.position = currentPosition;
    }
}