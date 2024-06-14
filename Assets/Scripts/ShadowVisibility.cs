using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowVisibility : MonoBehaviour
{
    public Transform player; 
    public float threshold = 0.08f; 
    private Renderer objectRenderer;
    private bool isTriggered = false; 
    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        objectRenderer.enabled = false;
    }
    
    void Update()
    {
        if (isTriggered)
            return;
        
        float distanceX = Mathf.Abs(transform.position.x - player.position.x);
        float distanceZ = Mathf.Abs(transform.position.z - player.position.z);
        
        if (distanceX <= threshold && distanceZ <= threshold)
        {
            objectRenderer.enabled = true;
            isTriggered = true;
        }
        else
        {
            objectRenderer.enabled = false;
        }  
    }
}
