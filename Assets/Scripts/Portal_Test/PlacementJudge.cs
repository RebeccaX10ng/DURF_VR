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
            
            PlacementManager manager = FindObjectOfType<PlacementManager>();
            
            if (manager != null)
            {
                manager.CubePlaced(gameObject);
                isPlaced = true;
            }
        }
    }
}