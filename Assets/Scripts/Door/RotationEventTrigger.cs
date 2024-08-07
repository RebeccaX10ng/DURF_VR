using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections;
public class RotationEventTrigger : MonoBehaviour
{
    public Light directionalLight;
    public GameObject door;
    public float lightXRotationThreshold = 3f;
    public float targetYRotationThreshold = 3f;

    public UnityEvent onRotationMatch;
    
    public GameObject objectToMove;
    public Vector3 moveAmount = new Vector3(0f, 0f, -3f);
    public float moveDuration = 2f;
    public GameObject collider;

    void Update()
    {
        float lightXRotation = NormalizeAngle(directionalLight.transform.eulerAngles.x);
        float targetYRotation = NormalizeAngle(door.transform.eulerAngles.y);

        if (IsWithinThreshold(lightXRotation, 90f, lightXRotationThreshold) &&
            IsWithinThreshold(targetYRotation, 90f, targetYRotationThreshold) ||
            IsWithinThreshold(targetYRotation, 270f, targetYRotationThreshold))
        {
            onRotationMatch.Invoke();
            StartCoroutine(MoveObject());
        }
    }

    float NormalizeAngle(float angle)
    {
        while (angle > 360f)
        {
            angle -= 360f;
        }
        while (angle < 0f)
        {
            angle += 360f;
        }
        return angle;
    }

    bool IsWithinThreshold(float value, float target, float threshold)
    {
        return Mathf.Abs(value - target) <= threshold || Mathf.Abs(value - (target + 360f)) <= threshold;
    }
    
    IEnumerator MoveObject()
    {
        Vector3 startPosition = objectToMove.transform.position;
        Vector3 endPosition = startPosition + moveAmount;
        float elapsedTime = 0f;
        
        while (elapsedTime < moveDuration)
        {
            objectToMove.transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        objectToMove.transform.position = endPosition;

        collider.SetActive(true);
    }
}