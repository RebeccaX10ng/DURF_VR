using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    public string targetTag = "Obstacle";
    private bool isColliding = false;
    public GameObject TunnelingVignette;

    private void Start()
    {
        TunnelingVignette.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            isColliding = true;
           TunnelingVignette.SetActive(true);
            Debug.Log("Collision Detected");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            isColliding = false;
            TunnelingVignette.SetActive(false);
        }
    }
}