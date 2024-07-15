using UnityEngine;
using UnityEngine.UI;

public class ScreenFade : MonoBehaviour
{
    public Image fadeImage;
    public float fadeSpeed = 1f;
    private float targetAlpha = 0f;

    private void Update()
    {
        Color color = fadeImage.color;
        color.a = Mathf.MoveTowards(color.a, targetAlpha, fadeSpeed * Time.deltaTime);
        fadeImage.color = color;
    }

    public void FadeToBlack()
    {
        targetAlpha = 1f;
    }

    public void FadeToClear()
    {
        targetAlpha = 0f;
    }
}