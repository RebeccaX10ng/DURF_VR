using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    public string targetTag = "Obstacle";
    private bool isColliding = false;
    private ScreenFade screenFade;

    private void Start()
    {
        screenFade = FindObjectOfType<ScreenFade>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            isColliding = true;
            screenFade.FadeToBlack();
            Debug.Log("Collision Detected");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            isColliding = false;
            screenFade.FadeToClear();
        }
    }
}