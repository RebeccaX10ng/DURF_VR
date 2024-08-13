using UnityEngine;

public class SpecialCollisionChecker : MonoBehaviour
{
    private int blockCollisionCount = 0;

    public bool IsCollidingWithTwoBlocks => blockCollisionCount >= 2;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Block"))
        {
            blockCollisionCount++;
            Debug.Log($"{gameObject.name} collided with a Block. Total: {blockCollisionCount}");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Block"))
        {
            blockCollisionCount--;
        }
    }
}