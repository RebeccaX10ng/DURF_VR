using System.Collections;
using UnityEngine;
using TMPro;

public class UITextController : MonoBehaviour
{
    public TMP_Text statusTexts;
    public TMP_Text environmentTexts;

    private bool isGlitching = false; 
    private Coroutine glitchCoroutine;
    
    public void StartGlitch()
    {
        if (!isGlitching)
        {
            isGlitching = true;
            glitchCoroutine = StartCoroutine(GlitchEffect());
        }
    }
    
    public void StatusStable()
    {
        StopGlitch(); 
        
        statusTexts.color = new Color32(91, 255, 83, 255); 
        statusTexts.text = "STABLE";
    }

    public void StatusUnstable()
    {
        StopGlitch(); 
        
        statusTexts.color = new Color32(255, 95, 84, 255);
        statusTexts.text = "UNSTABLE";
    }

    public void EnvironmentSafe()
    {
        StopGlitch(); 
        
        environmentTexts.color = new Color32(91, 255, 83, 255); 
        environmentTexts.text = "SAFE";
    }

    public void EnvironmentMedium()
    {
        StopGlitch(); 
        
        environmentTexts.color = new Color32(255, 255, 0, 255); 
        environmentTexts.text = "MEDIUM";
    }
    
    public void EnvironmentDangerous()
    {
        StopGlitch(); 
        
        environmentTexts.color = new Color32(255, 95, 84, 255); 
        environmentTexts.text = "DANGEROUS";
    }
   
    public void StopGlitch()
    {
        if (isGlitching)
        {
            isGlitching = false;
            if (glitchCoroutine != null)
            {
                StopCoroutine(glitchCoroutine);
            }
        }
    }
    
    //乱码
    IEnumerator GlitchEffect()
    {
        while (isGlitching)
        {
            statusTexts.color = Random.ColorHSV(); 
            statusTexts.text = GetRandomString(10);
            
            environmentTexts.color = Random.ColorHSV(); 
            environmentTexts.text = GetRandomString(10);
            
            yield return new WaitForSeconds(0.1f); 
        }
    }
    
    string GetRandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        char[] stringChars = new char[length];
        for (int i = 0; i < length; i++)
        {
            stringChars[i] = chars[Random.Range(0, chars.Length)];
        }
        return new string(stringChars);
    }
}
