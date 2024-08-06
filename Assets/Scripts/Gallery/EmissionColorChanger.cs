using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmissionColorChanger : MonoBehaviour
{
    public List<Material> targetMaterials; // List of materials to change emission color
    public float changeInterval = 1.0f; // Interval between changes
    public float maxEmissionValue = 1.0f; // Maximum emission value

    private bool isIncreasing = true;
    private float currentEmissionValue = 0.0f;

    void Start()
    {
        if (targetMaterials.Count == 0)
        {
            Debug.LogError("No materials assigned to targetMaterials list.");
            return;
        }
        
        StartCoroutine(ChangeEmissionColor());
    }

    IEnumerator ChangeEmissionColor()
    {
        while (true)
        {
            yield return new WaitForSeconds(changeInterval);
            UpdateEmissionColor();
        }
    }

    void UpdateEmissionColor()
    {
        if (isIncreasing)
        {
            currentEmissionValue += 0.1f;
            if (currentEmissionValue >= maxEmissionValue)
            {
                currentEmissionValue = maxEmissionValue;
                isIncreasing = false;
            }
        }
        else
        {
            currentEmissionValue -= 0.1f;
            if (currentEmissionValue <= 0.0f)
            {
                currentEmissionValue = 0.0f;
                isIncreasing = true;
            }
        }

        Color emissionColor = new Color(currentEmissionValue, currentEmissionValue, currentEmissionValue);

        foreach (var material in targetMaterials)
        {
            material.SetColor("_EmissionColor", emissionColor);
        }

        // Optionally update the global illumination if necessary
        foreach (Renderer renderer in FindObjectsOfType<Renderer>())
        {
            foreach (Material mat in renderer.sharedMaterials)
            {
                if (targetMaterials.Contains(mat))
                {
                    DynamicGI.SetEmissive(renderer, emissionColor);
                }
            }
        }
    }
}
