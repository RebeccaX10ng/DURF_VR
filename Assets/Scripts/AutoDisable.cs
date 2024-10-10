using UnityEngine;

public class AutoDisable : MonoBehaviour
{
    public float disableDelay = 2f; 
    
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