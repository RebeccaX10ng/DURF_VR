using UnityEngine;

public class CollisionChecker : MonoBehaviour
{
    public bool IsCollidingWithBlock { get; private set; } = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Block"))
        {
            IsCollidingWithBlock = true;
            Debug.Log($"{gameObject.name} collided with a Block");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Block"))
        {
            IsCollidingWithBlock = false;
        }
    }
}