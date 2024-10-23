using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using System.Collections;

public class HueShiftController : MonoBehaviour
{
    public Volume globalVolume;
    public VolumeProfile hueShiftProfile;  // 设置专门控制色相变换的配置文件
    public float hueSpeed = 15f;

    private ColorAdjustments colorAdjustments;
    private bool isShifting = false;
    private float originalHueShift;

    void Start()
    {
        if (globalVolume == null)
        {
            Debug.LogError("Global Volume is not assigned.");
        }

        if (hueShiftProfile == null)
        {
            Debug.LogError("Hue Shift Profile is not assigned.");
        }

        StartCoroutine(DelayedStartHueShift(2f));
    }

    void Update()
    {
        if (isShifting)
        {
            // 持续更新色相值
            colorAdjustments.hueShift.value += hueSpeed * Time.deltaTime;

            // 限制色相值在 -180 到 180 之间
            if (colorAdjustments.hueShift.value >= 180f)
            {
                colorAdjustments.hueShift.value = -180f;
            }
        }
    }

    private IEnumerator DelayedStartHueShift(float delay)
    {
        yield return new WaitForSeconds(delay);

        // 应用色相变换的配置文件
        SetHueShiftProfile(hueShiftProfile);
    }

    private void SetHueShiftProfile(VolumeProfile profile)
    {
        globalVolume.profile = profile; 

        // 获取 Color Adjustments 组件并记录原始色相值
        if (profile.TryGet(out colorAdjustments))
        {
            originalHueShift = colorAdjustments.hueShift.value; // 记录初始色相值
            isShifting = true; // 开始色相变化
        }
        else
        {
            Debug.LogError("Color Adjustments component not found in target profile.");
        }
    }

    public void StopHueShift()
    {
        isShifting = false;
        if (colorAdjustments != null)
        {
            colorAdjustments.hueShift.value = originalHueShift; // 恢复原始色相值
        }
    }
}