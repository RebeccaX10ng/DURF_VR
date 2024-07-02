using UnityEngine;

public class CubeMovement : MonoBehaviour
{
    public GameObject[] tallCubes;
    public GameObject[] middleCubes;
    public GameObject[] lowCubes;

    // Tall组的移动范围和速度
    public float tallMoveRange = 0.1f;
    public float tallMoveSpeed = 1f;
    public float tallPhaseOffset = 0f;

    // Middle组的移动范围和速度
    public float middleMoveRange = 0.05f;
    public float middleMoveSpeed = 0.75f;
    public float middlePhaseOffset = Mathf.PI / 2;

    // Low组的移动范围和速度
    public float lowMoveRange = 0.025f;
    public float lowMoveSpeed = 0.5f;
    public float lowPhaseOffset = Mathf.PI;

    // 子物体的初始位置
    private Vector3[] tallInitialPositions;
    private Vector3[] middleInitialPositions;
    private Vector3[] lowInitialPositions;

    void Start()
    {
        // 初始化Tall组
        tallInitialPositions = new Vector3[tallCubes.Length];
        for (int i = 0; i < tallCubes.Length; i++)
        {
            tallInitialPositions[i] = tallCubes[i].transform.localPosition;
        }

        // 初始化Middle组
        middleInitialPositions = new Vector3[middleCubes.Length];
        for (int i = 0; i < middleCubes.Length; i++)
        {
            middleInitialPositions[i] = middleCubes[i].transform.localPosition;
        }

        // 初始化Low组
        lowInitialPositions = new Vector3[lowCubes.Length];
        for (int i = 0; i < lowCubes.Length; i++)
        {
            lowInitialPositions[i] = lowCubes[i].transform.localPosition;
        }
    }

    void Update()
    {
        // 更新Tall组
        for (int i = 0; i < tallCubes.Length; i++)
        {
            float newY = tallInitialPositions[i].y + Mathf.Sin(Time.time * tallMoveSpeed + tallPhaseOffset) * tallMoveRange;
            tallCubes[i].transform.localPosition = new Vector3(tallInitialPositions[i].x, newY, tallInitialPositions[i].z);
        }

        // 更新Middle组
        for (int i = 0; i < middleCubes.Length; i++)
        {
            float newY = middleInitialPositions[i].y + Mathf.Sin(Time.time * middleMoveSpeed + middlePhaseOffset) * middleMoveRange;
            middleCubes[i].transform.localPosition = new Vector3(middleInitialPositions[i].x, newY, middleInitialPositions[i].z);
        }

        // 更新Low组
        for (int i = 0; i < lowCubes.Length; i++)
        {
            float newY = lowInitialPositions[i].y + Mathf.Sin(Time.time * lowMoveSpeed + lowPhaseOffset) * lowMoveRange;
            lowCubes[i].transform.localPosition = new Vector3(lowInitialPositions[i].x, newY, lowInitialPositions[i].z);
        }
    }
}