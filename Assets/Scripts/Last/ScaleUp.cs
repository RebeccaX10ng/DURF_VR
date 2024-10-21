using UnityEngine;

public class ScaleUp : MonoBehaviour
{
    public float scaleFactor = 1.01f; 

    void Update()
    {
        transform.localScale *= scaleFactor; 
    }
}