using UnityEngine;

public class AreaCheckerManager : MonoBehaviour
{
    public PaintingAreaChecker[] areaCheckers; 
    public GameObject objectToHide; 
    public GameObject collisionTeleporter;
    public AudioSource audioSouce;
    public AudioClip successSound;

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
            Debug.Log("Success! All objects are in their target areas.");
            audioSouce.PlayOneShot(successSound);
            objectToHide.GetComponent<Renderer>().enabled = false;
            collisionTeleporter.SetActive(true);
        }
    }
}