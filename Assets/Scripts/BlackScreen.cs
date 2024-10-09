using UnityEngine;
using UnityEngine.UI;

public class BlackScreen : MonoBehaviour
{
    public GameObject blackScreenUI;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            blackScreenUI.SetActive(true);
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            blackScreenUI.SetActive(false);
        }
    }
}