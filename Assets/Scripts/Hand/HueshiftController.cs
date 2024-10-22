using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class HueShiftController : MonoBehaviour
{
    public Volume globalVolume;
    public float hueSpeed = 15f; 

    private ColorAdjustments colorAdjustments;
    private bool isShifting = false;
    
    void Start()
    {
        if (globalVolume.profile.TryGet(out colorAdjustments))
        {
            Debug.Log("Color Adjustments component found.");
        }
        else
        {
            Debug.LogError("Color Adjustments component not found in Global Volume.");
        }
    }

    void Update()
    {
        if (isShifting)
        {
            colorAdjustments.hueShift.value += hueSpeed * Time.deltaTime;
            
            if (colorAdjustments.hueShift.value >= 180f)
            {
                colorAdjustments.hueShift.value = -180f;
            }
        }
    }

    
    public void StartHueShift()
    {
        isShifting = true;
    }

    
    public void StopHueShift()
    {
        isShifting = false;
        colorAdjustments.hueShift.value = 0f;
    }
}