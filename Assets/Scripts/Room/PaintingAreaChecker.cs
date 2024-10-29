using UnityEngine;

public class PaintingAreaChecker : MonoBehaviour
{
    public GameObject targetObject;
    public Material newMaterial;
    private Material originalMaterial;
    public bool isTargetInside = false;
    //private bool hasLoggedSuccess = false;

    private bool enableCheck = true;
    

    void Start()
    {
        if (targetObject != null)
        {
            MeshRenderer renderer = targetObject.GetComponent<MeshRenderer>();
            if (renderer != null)
            {
                originalMaterial = renderer.material;
            }
        }
    }

    public void DisableCheck()
    {
        enableCheck = false;
    }

    public void EnableCheck()
    {
        enableCheck = true;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (enableCheck && other.gameObject == targetObject)
        {
            isTargetInside = true;
            //Debug.Log($"Success! {targetObject.name} is fully in the target area.");
            //hasLoggedSuccess = true;

            MeshRenderer renderer = targetObject.GetComponent<MeshRenderer>();
            if (renderer != null && newMaterial != null)
            {
                renderer.material = newMaterial;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == targetObject)
        {
            isTargetInside = false;
            //hasLoggedSuccess = false;
            
            MeshRenderer renderer = targetObject.GetComponent<MeshRenderer>();
            if (renderer != null && originalMaterial != null)
            {
                renderer.material = originalMaterial;
            }
        }
    }
}
