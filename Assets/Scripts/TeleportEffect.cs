using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using System.Collections.Generic;

public class TeleportEffect : MonoBehaviour
{
    public List<VolumeProfile> globalProfiles;  // 存储多个 Global Volume Profiles
    public Volume targetVolume; 
    private int currentProfileIndex = 0;
    
    private ColorAdjustments colorAdjustments;
    private LensDistortion lensDistortion;
    private ChromaticAberration chromaticAberration;

    private bool teleport = false;
    private float originalExposure;
    private float originalLensDistortionIntensity;
    private float originalLensDistortionScale;
    private float originalChromaticAberration;

    public void Teleport()
    {
        teleport = true;
    }

    void Start()
    {
        SetVolumeProfile(globalProfiles[currentProfileIndex]);
    }

    void Update()
    {
        if (teleport)
        {
            StartCoroutine(TeleportEffectRoutine());
            teleport = false;
        }
    }
    
    private void SetVolumeProfile(VolumeProfile profile)
    {
        if (targetVolume != null)
        {
            targetVolume.profile = profile; // 将 VolumeProfile 应用到目标 Volume 上
        }

        if (profile.TryGet(out colorAdjustments) &&
            profile.TryGet(out lensDistortion) &&
            profile.TryGet(out chromaticAberration))
        {
            originalExposure = colorAdjustments.postExposure.value;
            originalLensDistortionIntensity = lensDistortion.intensity.value;
            originalLensDistortionScale = lensDistortion.scale.value;
            originalChromaticAberration = chromaticAberration.intensity.value;
        }
    }
    
    // 切换到下一个 Global Volume
    private void SwitchToNextVolume()
    {
        int previousProfileIndex = currentProfileIndex;
        
        currentProfileIndex = (currentProfileIndex + 1) % globalProfiles.Count;  // 循环切换
        SetVolumeProfile(globalProfiles[currentProfileIndex]);
        
        StartCoroutine(RestorePreviousVolumeProfile(previousProfileIndex));
    }
    
    private System.Collections.IEnumerator RestorePreviousVolumeProfile(int profileIndex)
    {
        // 假设你想在切换之后等待 2 秒再还原
        yield return new WaitForSeconds(2f);

        // 获取之前的 VolumeProfile
        VolumeProfile previousProfile = globalProfiles[profileIndex];

        // 还原之前 Profile 的效果值
        if (previousProfile.TryGet(out ColorAdjustments previousColorAdjustments))
        {
            previousColorAdjustments.postExposure.value = originalExposure;
        }
        if (previousProfile.TryGet(out LensDistortion previousLensDistortion))
        {
            previousLensDistortion.intensity.value = originalLensDistortionIntensity;
            previousLensDistortion.scale.value = originalLensDistortionScale;
        }
        if (previousProfile.TryGet(out ChromaticAberration previousChromaticAberration))
        {
            previousChromaticAberration.intensity.value = originalChromaticAberration;
        }
    }

    System.Collections.IEnumerator TeleportEffectRoutine()
    {
        float duration = 2f; 
        float elapsedTime = 0f;

        // Step 1: 逐渐增加到最大效果
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / duration;

            // 渐变到最大效果
            lensDistortion.intensity.value = Mathf.Lerp(originalLensDistortionIntensity, -0.5f, progress);  
            lensDistortion.scale.value = Mathf.Lerp(originalLensDistortionScale, 0.8f, progress);  
            chromaticAberration.intensity.value = Mathf.Lerp(originalChromaticAberration, 1f, progress);
            colorAdjustments.postExposure.value = Mathf.Lerp(originalExposure, 10f, progress);

            yield return null;
        }

        // Step 2: 切换到下一个 Global Volume，保持当前效果
        SwitchToNextVolume();
        lensDistortion.intensity.value = -0.5f;
        lensDistortion.scale.value = 0.8f;
        chromaticAberration.intensity.value = 1f;
        colorAdjustments.postExposure.value = 10f;

        // 短暂保持效果
        yield return new WaitForSeconds(0.5f);

        // Step 3: 逐渐减小到原始值
        elapsedTime = 0f;
        duration = 1f;  // 恢复过程持续1秒
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / duration;

            // 恢复到原始效果
            colorAdjustments.postExposure.value = Mathf.Lerp(10f, originalExposure, progress);
            lensDistortion.intensity.value = Mathf.Lerp(-0.5f, originalLensDistortionIntensity, progress);
            lensDistortion.scale.value = Mathf.Lerp(0.8f, originalLensDistortionScale, progress);
            chromaticAberration.intensity.value = Mathf.Lerp(1f, originalChromaticAberration, progress);

            yield return null;
        }
    }
}
