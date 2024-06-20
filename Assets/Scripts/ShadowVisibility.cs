using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowVisibility : MonoBehaviour
{
    public Transform player;
    public float threshold = 0.08f;
    public GameObject objectToActivate1;
    public GameObject objectToDeactivate1;
    public GameObject objectToActivate2;
    public GameObject objectToDeactivate2;
    public Material newMaterial1;
    public Material newMaterial2;
    public Camera mainCamera;
    public Camera secondaryCamera;
    public float fadeDuration = 1.0f;

    private Renderer objectRenderer;
    private Transform objectTransform;
    private bool isTriggered = false;
    private bool showShadow = false;
    private int triggerCount = 0; // 用于跟踪触发次数

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        objectTransform = transform;
        objectRenderer.enabled = false;
        objectToActivate1.SetActive(false);
        secondaryCamera.gameObject.SetActive(false);
    }

    void Update()
    {
        float distance = Vector3.Distance(objectTransform.position, player.position);
        
        if (!isTriggered && distance <= threshold)
        {
            objectToDeactivate1.SetActive(false);
            showShadow = true;
            isTriggered = true;
            triggerCount++;
            TriggerAction();
        }

        if (showShadow)
        {
            objectRenderer.enabled = true;
        }
        
        if (distance > threshold)
        {
            objectToDeactivate1.SetActive(true);
            if (isTriggered)
            {
                isTriggered = false;
            }
        }
    }

    void TriggerAction()
    {
        switch (triggerCount)
        {
            case 1:
                objectToActivate1.SetActive(true);
                //objectToDeactivate1.SetActive(false);
                GetComponent<Renderer>().material = newMaterial1;
                break;
            case 2:
                objectToActivate2.SetActive(true);
                objectToDeactivate2.SetActive(false);
                GetComponent<Renderer>().material = newMaterial2;
                break;
            case 3:
                StartCoroutine(SwitchCameraWithFade());
                break;
            default:
                break;
        }
    }

    IEnumerator SwitchCameraWithFade()
    {
        // Start fade-out
        yield return StartCoroutine(Fade(1.0f, fadeDuration));

        // Switch cameras
        mainCamera.gameObject.SetActive(false);
        secondaryCamera.gameObject.SetActive(true);

        // Start fade-in
        yield return StartCoroutine(Fade(0.0f, fadeDuration));
    }

    IEnumerator Fade(float targetAlpha, float duration)
    {
        float currentAlpha = 1.0f - targetAlpha;
        CanvasGroup canvasGroup = FindObjectOfType<CanvasGroup>();

        if (canvasGroup == null)
        {
            GameObject fadeCanvas = new GameObject("FadeCanvas");
            Canvas canvas = fadeCanvas.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvasGroup = fadeCanvas.AddComponent<CanvasGroup>();
            fadeCanvas.AddComponent<CanvasRenderer>();
            fadeCanvas.AddComponent<UnityEngine.UI.Image>();
            fadeCanvas.GetComponent<UnityEngine.UI.Image>().color = Color.black;
        }

        float elapsedTime = 0.0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(currentAlpha, targetAlpha, elapsedTime / duration);
            yield return null;
        }
        canvasGroup.alpha = targetAlpha;
    }
}
