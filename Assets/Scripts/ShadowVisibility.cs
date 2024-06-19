using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowVisibility : MonoBehaviour
{
    public Transform player; 
    public float threshold = 0.08f;
    public GameObject mask;
    private Renderer objectRenderer;
    private Transform objectTransform;
    private bool isTriggered = false; 
    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        objectTransform = transform;
        objectRenderer.enabled = false;
        mask.SetActive(false);
    }
    
    void Update()
    {
        float distance = Vector3.Distance(objectTransform.position, player.position);
        
        if (!isTriggered && distance <= threshold)
        {
            objectRenderer.enabled = true;
            isTriggered = true;
        }
        mask.SetActive(distance <= threshold);
    }
}
