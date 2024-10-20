using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BlackScreen : MonoBehaviour
{
    public GameObject blackScreenUI;
    public UITextController uiTextController;

    private string originalStatusTextContent;
    private string originalEnvironmentTextContent;
    private Color originalStatusTextColor;
    private Color originalEnvironmentTextColor;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            if (uiTextController != null)
            {
                originalStatusTextContent = uiTextController.statusTexts.text;
                originalEnvironmentTextContent = uiTextController.environmentTexts.text;
                originalStatusTextColor = uiTextController.statusTexts.color;
                originalEnvironmentTextColor = uiTextController.environmentTexts.color;
            }
            
            blackScreenUI.SetActive(true);
            uiTextController.StartGlitch();
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            blackScreenUI.SetActive(false);
            uiTextController.StopGlitch(); // 停止显示乱码

            // 恢复原始的文本内容和颜色
            uiTextController.statusTexts.text = originalStatusTextContent;
            uiTextController.statusTexts.color = originalStatusTextColor;
            uiTextController.environmentTexts.text = originalEnvironmentTextContent;
            uiTextController.environmentTexts.color = originalEnvironmentTextColor;
        }
    }
}