using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementJudge : MonoBehaviour
{
    public Material placedMaterial; 
    private bool isPlaced = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!isPlaced && other.CompareTag("Judge Area"))
        {
            Renderer renderer = GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material = placedMaterial;
            }
            
            transform.SetParent(other.transform);
            
            MonoBehaviour[] scripts = GetComponents<MonoBehaviour>();
            foreach (MonoBehaviour script in scripts)
            {
                script.enabled = false;
            }

            isPlaced = true; 
        }
    }
}