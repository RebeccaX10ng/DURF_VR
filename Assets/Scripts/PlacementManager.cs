using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    public GameObject shadow;
    public Material[] placedMaterials; 
    public GameObject[] objectsToActivate; 
    public GameObject[] objectsToDeactivate; 

    private int placedCount = 0; 

    public void CubePlaced(GameObject cube)
    {
        Renderer renderer = shadow.GetComponent<Renderer>();
        if (renderer != null && placedCount < placedMaterials.Length)
        {
            renderer.material = placedMaterials[placedCount];
        }

        if (placedCount < objectsToActivate.Length)
        {
            objectsToActivate[placedCount].SetActive(true);
        }
        if (placedCount < objectsToDeactivate.Length)
        {
            objectsToDeactivate[placedCount].SetActive(false);
        }

        placedCount++;
        
        MonoBehaviour[] scripts = cube.GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour script in scripts)
        {
            script.enabled = false;
        }

        cube.transform.SetParent(null); 
    }
}