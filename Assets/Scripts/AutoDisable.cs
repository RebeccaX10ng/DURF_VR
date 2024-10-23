using UnityEngine;

public class AutoDisable : MonoBehaviour
{
    public float disableDelay = 3f; 
    
    void OnEnable()
    {
        StartCoroutine(DisableAfterDelay());
    }
    
    private System.Collections.IEnumerator DisableAfterDelay()
    {
        yield return new WaitForSeconds(disableDelay);
        
        gameObject.SetActive(false);
    }
}