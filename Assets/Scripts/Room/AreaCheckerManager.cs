using UnityEngine;

public class AreaCheckerManager : MonoBehaviour
{
    public PaintingAreaChecker[] areaCheckers; 
    public GameObject objectToHide; 
    public GameObject collisionTeleporter;
    public AudioSource audioSouce;
    public AudioClip successSound;
    private bool hasPlayed = false;

    void Update()
    {
        CheckAllAreas();
    }

    private void CheckAllAreas()
    {
        bool allInTarget = true; 

        foreach (var checker in areaCheckers)
        {
            if (!checker.IsTargetInside)
            {
                allInTarget = false;
                break;
            }
        }

        if (allInTarget)
        {
            if (!hasPlayed)
            {
                audioSouce.PlayOneShot(successSound);
                hasPlayed = true;
                Debug.Log("Success! All objects are in their target areas.");
            }

            objectToHide.SetActive(false);
                
            collisionTeleporter.SetActive(true);
        }
    }
}