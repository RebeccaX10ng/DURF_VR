using UnityEngine;
using System.Collections;

public class ObjectController : MonoBehaviour
{
    // Public references to the objects you want to rotate and move
    public GameObject objectToRotateZ;
    public GameObject objectToRotateX;
    public GameObject[] objectsToMoveY;
    public AudioClip audioClip;

    // Public function to execute the actions
    public void PerformActions()
    {
        // Start coroutines to smoothly rotate the objects
        StartCoroutine(RotateObjectZ(objectToRotateZ, new Vector3(0, 0, -50), 2f));
        StartCoroutine(RotateObjectX(objectToRotateX, new Vector3(-50, 0, 0), 2f));

        // Play the audio clip
        if (audioClip != null)
        {
            AudioSource.PlayClipAtPoint(audioClip, transform.position);
        }
    }

    // Coroutine to rotate an object on the Z-axis smoothly over time
    private IEnumerator RotateObjectZ(GameObject obj, Vector3 rotation, float duration)
    {
        Quaternion initialRotation = obj.transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(obj.transform.eulerAngles + rotation);
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            obj.transform.rotation = Quaternion.Lerp(initialRotation, targetRotation, elapsedTime / duration);
            yield return null;
        }

        obj.transform.rotation = targetRotation;
        
        StartCoroutine(MoveObjectsY());
    }

    // Coroutine to rotate an object on the X-axis smoothly over time
    private IEnumerator RotateObjectX(GameObject obj, Vector3 rotation, float duration)
    {
        Quaternion initialRotation = obj.transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(obj.transform.eulerAngles + rotation);
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            obj.transform.rotation = Quaternion.Lerp(initialRotation, targetRotation, elapsedTime / duration);
            yield return null;
        }

        obj.transform.rotation = targetRotation;
    }

    // Coroutine to move objects along the Y-axis over 2 seconds
    private IEnumerator MoveObjectsY()
    {
        float duration = 1.5f;
        float elapsedTime = 0f;

        Vector3[] startPositions = new Vector3[objectsToMoveY.Length];
        Vector3[] targetPositions = new Vector3[objectsToMoveY.Length];

        // Store the start and target positions for each object
        for (int i = 0; i < objectsToMoveY.Length; i++)
        {
            startPositions[i] = objectsToMoveY[i].transform.position;
            targetPositions[i] = startPositions[i] + new Vector3(0, 4, 0);
        }

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;

            // Interpolate positions for each object
            for (int i = 0; i < objectsToMoveY.Length; i++)
            {
                objectsToMoveY[i].transform.position = Vector3.Lerp(startPositions[i], targetPositions[i], t);
            }

            yield return null;
        }

        // Ensure objects reach their final positions
        for (int i = 0; i < objectsToMoveY.Length; i++)
        {
            objectsToMoveY[i].transform.position = targetPositions[i];
        }
    }
}
