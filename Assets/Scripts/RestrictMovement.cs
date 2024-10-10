using UnityEngine;

public class RestrictMovement : MonoBehaviour
{
    private float initialX;

    void Start()
    {
        initialX = transform.position.x;
    }

    void Update()
    {
        Vector3 currentPosition = transform.position;
        currentPosition.x = initialX;
        transform.position = currentPosition;
    }
}