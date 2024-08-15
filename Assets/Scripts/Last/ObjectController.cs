using UnityEngine;
using System.Collections;

public class ObjectController : MonoBehaviour
{
    // Public references to the objects you want to move
    public GameObject objectToMoveSmall;
    public GameObject[] objectsToMoveLarge;
    public GameObject objectToDisappearWithTransparency;
    public GameObject[] objectsToDisappearWithScale;
    public AudioSource soundEffect;
    public AudioSource narration;
    public AudioSource bgmToStop;
    public AudioSource bgmToPlay;
    
    //public GameObject objectToActivateAfterEvents; // 事件完成后激活的物体
    public GameObject[] textObjects; // 文本对象数组
    
    public float fadeDuration = 2f; // 渐变持续时间
    private Renderer objectRenderer; // 物体的 Renderer
    private Material objectMaterial; // 物体的材质

    void Start()
    {
        objectRenderer = objectToDisappearWithTransparency.GetComponent<Renderer>();
        objectMaterial = objectRenderer.material;
        StartCoroutine(FadeOut());
        PerformActions();
    }

    // Public function to execute the actions
    void PerformActions()
    {
        // Start coroutines to move the objects
        StartCoroutine(MoveObjectSmall(objectToMoveSmall, 0.5f, 3f));
        StartCoroutine(MoveObjectsLarge(objectsToMoveLarge, 3f, 3f));
        StartCoroutine(ShrinkAndDisappear(objectsToDisappearWithScale, 3f));
        
        soundEffect.Play();
        narration.Play();
    }

    private IEnumerator ShrinkAndDisappear(GameObject[] objects, float shrinkDuration)
    {
        // Store the initial scales of each object
        Vector3[] initialScales = new Vector3[objects.Length];
        for (int i = 0; i < objects.Length; i++)
        {
            initialScales[i] = objects[i].transform.localScale;
        }

        // Perform the shrinking effect over the specified duration
        for (float t = 0; t < shrinkDuration; t += Time.deltaTime)
        {
            float progress = t / shrinkDuration;

            // Update the scale of each object in the array
            for (int i = 0; i < objects.Length; i++)
            {
                objects[i].transform.localScale = Vector3.Lerp(initialScales[i], Vector3.zero, progress);
            }

            yield return null;
        }

        // Ensure all objects are fully shrunk and optionally deactivate them
        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].transform.localScale = Vector3.zero;
            objects[i].SetActive(false);
        }
    }
    private IEnumerator FadeOut()
    {
        Color initialColor = objectMaterial.color; // 初始颜色
        float elapsedTime = 0f;

        // 在 fadeDuration 时间内逐渐改变透明度
        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(1, 0, elapsedTime / fadeDuration);
            objectMaterial.color = new Color(initialColor.r, initialColor.g, initialColor.b, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 确保透明度为 0 并禁用物体
        objectMaterial.color = new Color(initialColor.r, initialColor.g, initialColor.b, 0);
        objectToDisappearWithTransparency.SetActive(false); // 可选：禁用物体
    }
    
    // Coroutine to move the small object upward by 0.5 units over 2 seconds
    private IEnumerator MoveObjectSmall(GameObject obj, float moveAmount, float duration)
    {
        Vector3 startPosition = obj.transform.position;
        Vector3 targetPosition = startPosition + new Vector3(0, moveAmount, 0);
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            obj.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
            yield return null;
        }

        obj.transform.position = targetPosition;
        
        //这里写最后要做的事件！！！！！
        bgmToStop.Stop();
        StartCoroutine(DisplayText());
    }

    // Coroutine to move large objects upward by 3 units over 2 seconds
    private IEnumerator MoveObjectsLarge(GameObject[] objects, float moveAmount, float duration)
    {
        float elapsedTime = 0f;

        Vector3[] startPositions = new Vector3[objects.Length];
        Vector3[] targetPositions = new Vector3[objects.Length];

        // Store the start and target positions for each object
        for (int i = 0; i < objects.Length; i++)
        {
            startPositions[i] = objects[i].transform.position;
            targetPositions[i] = startPositions[i] + new Vector3(0, moveAmount, 0);
        }

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;

            // Interpolate positions for each object
            for (int i = 0; i < objects.Length; i++)
            {
                objects[i].transform.position = Vector3.Lerp(startPositions[i], targetPositions[i], t);
            }

            yield return null;
        }

        // Ensure objects reach their final positions
        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].transform.position = targetPositions[i];
        }
    }
    
    private IEnumerator DisplayText()
    {
        // 等待2秒
        yield return new WaitForSeconds(3f);

        // 激活物体
        bgmToPlay.Play();

        yield return new WaitForSeconds(0.5f);

        // 轮流显示文本对象，每个间隔1.5秒
        foreach (GameObject textObject in textObjects)
        {
            MeshRenderer meshRenderer = textObject.GetComponent<MeshRenderer>();
            if (meshRenderer != null)
            {
                meshRenderer.enabled = true;
            }
            yield return new WaitForSeconds(3f);
            if (meshRenderer != null)
            {
                meshRenderer.enabled = false;
            }
        }
    }
}
