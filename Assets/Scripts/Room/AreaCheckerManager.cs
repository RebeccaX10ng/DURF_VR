using UnityEngine;

public class AreaCheckerManager : MonoBehaviour
{
    public PaintingAreaChecker[] areaCheckers;
    public GameObject objectToHide;
    public GameObject collisionTeleporter;
    public AudioSource audioSouce;
    public AudioClip successSound;
    private bool hasPlayed = false;

    private float checkInterval = 0.1f; // 检查间隔时间
    private float lastCheckTime = 0f;

    void Update()
    {
        // 降低检查频率
        if (Time.time - lastCheckTime >= checkInterval)
        {
            lastCheckTime = Time.time;
            CheckAllAreas();
        }
    }

    private void CheckAllAreas()
    {
        bool allInTarget = true;

        foreach (var checker in areaCheckers)
        {
            if (!checker.isTargetInside) // 使用公共属性而不是字段
            {
                allInTarget = false;
                break;
            }
        }

        if (allInTarget && !hasPlayed)
        {
            audioSouce.PlayOneShot(successSound);
            hasPlayed = true;
            Debug.Log("Success! All objects are in their target areas.");

            // 隐藏物体并激活传送器
            objectToHide.SetActive(false);
            collisionTeleporter.SetActive(true);
        }
    }
}