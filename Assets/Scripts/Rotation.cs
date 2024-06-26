using UnityEngine;

public class Rotation : MonoBehaviour
{
    // 旋转速度
    public float rotationSpeed = 10.0f;

    // Update is called once per frame
    void Update()
    {
        // 计算旋转角度
        float rotationAngle = rotationSpeed * Time.deltaTime;
        
        // 执行旋转
        transform.Rotate(Vector3.up, rotationAngle);
    }
}