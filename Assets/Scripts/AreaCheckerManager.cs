using UnityEngine;

public class AreaCheckerManager : MonoBehaviour
{
    public PaintingAreaChecker[] areaCheckers; 
    public GameObject objectToHide; 
    public CollisionTeleporter collisionTeleporter; 

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
            objectToHide.GetComponent<Renderer>().enabled = false;
            collisionTeleporter.enabled = true;
        }
    }
}