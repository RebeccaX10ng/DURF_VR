using UnityEngine;

public class CameraMoveXR : MonoBehaviour
{
    [SerializeField]
    private float elevationSpeed = 5.0f;

    private void Update()
    {
        // 获取 Elevation 输入轴的值
        float elevationInput = Input.GetAxis("Elevation");

        // 使用该值移动相机
        Vector3 movement = new Vector3(0, elevationInput * elevationSpeed * Time.deltaTime, 0);
        transform.Translate(movement);
    }
}