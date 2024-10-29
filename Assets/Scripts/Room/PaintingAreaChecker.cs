using UnityEngine;

public class PaintingAreaChecker : MonoBehaviour
{
    public GameObject targetObject;
    public Material newMaterial;
    private Material originalMaterial;
    public bool isTargetInside = false;
    private bool hasLoggedSuccess = false;

    private bool enableCheck = true;
    
    private float checkInterval = 0.1f; // Frequency of check
    private float lastCheckTime = 0f;

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
    
    private void OnTriggerStay(Collider other)
    {
        if (enableCheck)
        {
            if (other.gameObject != targetObject || Time.time - lastCheckTime < checkInterval) return;
            lastCheckTime = Time.time;

            Collider targetCollider = targetObject.GetComponent<Collider>();
            Bounds targetBounds = targetCollider.bounds;
            Bounds triggerBounds = GetComponent<Collider>().bounds;

            bool wasTargetInside = isTargetInside;
            isTargetInside = triggerBounds.Contains(targetBounds.min) && triggerBounds.Contains(targetBounds.max);

            if (isTargetInside && !wasTargetInside && !hasLoggedSuccess)
            {
                Debug.Log($"Success! {targetObject.name} is fully in the target area.");
                hasLoggedSuccess = true;

                MeshRenderer renderer = targetObject.GetComponent<MeshRenderer>();
                if (renderer != null && newMaterial != null)
                {
                    renderer.material = newMaterial;
                }
            }
            else if (!isTargetInside && wasTargetInside)
            {
                Debug.Log($"{targetObject.name} is partially outside the target area.");
                hasLoggedSuccess = false;

                MeshRenderer renderer = targetObject.GetComponent<MeshRenderer>();
                if (renderer != null && originalMaterial != null)
                {
                    renderer.material = originalMaterial;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == targetObject)
        {
            isTargetInside = false;
            hasLoggedSuccess = false;
            
            MeshRenderer renderer = targetObject.GetComponent<MeshRenderer>();
            if (renderer != null && originalMaterial != null)
            {
                renderer.material = originalMaterial;
            }
        }
    }
}
