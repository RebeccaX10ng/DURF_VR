using UnityEngine;
using System.Collections;

public class MoveDown : MonoBehaviour
{
    public float targetYPosition = -24f; 
    public float duration = 3f;
    public GameObject disableObject;

    private Vector3 initialPosition;
    private Vector3 targetPosition;

    void Start()
    {
        initialPosition = transform.position;
        
        targetPosition = new Vector3(transform.position.x, transform.position.y + targetYPosition, transform.position.z);
        
        StartCoroutine(MoveObjectDown());
    }

    IEnumerator MoveObjectDown()
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(initialPosition, targetPosition, elapsedTime / duration);
            
            elapsedTime += Time.deltaTime;
            
            yield return null;
        }
        
        transform.position = targetPosition;
        disableObject.SetActive(false);
    }
}