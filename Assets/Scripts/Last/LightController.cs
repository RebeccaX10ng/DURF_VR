using UnityEngine;
using System.Collections;

public class LightController : MonoBehaviour
{
    // Reference to the Light component you want to control
    public Light targetLight;

    // Public function to start the intensity transition
    void Start()
    {
        StartCoroutine(AdjustLightIntensity(targetLight, 0.08f, 2f));
    }

    // Coroutine to gradually change the light intensity
    private IEnumerator AdjustLightIntensity(Light light, float targetIntensity, float duration)
    {
        float startIntensity = light.intensity;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            light.intensity = Mathf.Lerp(startIntensity, targetIntensity, elapsedTime / duration);
            yield return null;
        }

        light.intensity = targetIntensity;
    }
}