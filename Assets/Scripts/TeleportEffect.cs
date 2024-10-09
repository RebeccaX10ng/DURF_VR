using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class TeleportEffect : MonoBehaviour
{
    public Volume globalVolume;
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
        if (globalVolume.profile.TryGet(out colorAdjustments) &&
            globalVolume.profile.TryGet(out lensDistortion) &&
            globalVolume.profile.TryGet(out chromaticAberration))
        {
            originalExposure = colorAdjustments.postExposure.value;
            originalLensDistortionIntensity = lensDistortion.intensity.value;
            originalLensDistortionScale = lensDistortion.scale.value;
            originalChromaticAberration = chromaticAberration.intensity.value;
        }
    }

    void Update()
    {
        if (teleport)
        {
            StartCoroutine(TeleportEffectRoutine());
            teleport = false;
        }
    }

    System.Collections.IEnumerator TeleportEffectRoutine()
    {
        float duration = 2f; 
        float elapsedTime = 0f;
        
        float randomHue = Random.Range(-180f, 180f);
        colorAdjustments.hueShift.value = randomHue;
        
        
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / duration;
            
            lensDistortion.intensity.value = Mathf.Lerp(originalLensDistortionIntensity, -0.5f, progress);  
            lensDistortion.scale.value = Mathf.Lerp(originalLensDistortionScale, 0.8f, progress);  
            chromaticAberration.intensity.value = Mathf.Lerp(originalChromaticAberration, 1f, progress);
            colorAdjustments.postExposure.value = Mathf.Lerp(originalExposure, 10f, progress);

            yield return null;
        }

        // 持续短暂的时间（可选）
        yield return new WaitForSeconds(0.2f);

        // 恢复初始值
        elapsedTime = 0f;
        duration = 1f;  // 恢复过程持续1秒
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / duration;

            
            colorAdjustments.postExposure.value = Mathf.Lerp(10f, originalExposure, progress);
            lensDistortion.intensity.value = Mathf.Lerp(-0.5f, originalLensDistortionIntensity, progress);
            lensDistortion.scale.value = Mathf.Lerp(0.8f, originalLensDistortionScale, progress);
            chromaticAberration.intensity.value = Mathf.Lerp(1f, originalChromaticAberration, progress);

            yield return null;
        }
        colorAdjustments.hueShift.value = 0f;
    }
}
