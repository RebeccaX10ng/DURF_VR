using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BlackScreen : MonoBehaviour
{
    public GameObject blackScreenUI;
    public UITextController uiTextController;

    private TMP_Text originalStatusText;
    private TMP_Text originalEnvironmentText;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            originalStatusText = uiTextController.statusTexts;
            originalEnvironmentText = uiTextController.environmentTexts;
            
            blackScreenUI.SetActive(true);
            uiTextController.StartGlitch();
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            blackScreenUI.SetActive(false);
            uiTextController.StopGlitch();
            
            uiTextController.statusTexts = originalStatusText;
            uiTextController.environmentTexts = originalEnvironmentText;
        }
    }
}