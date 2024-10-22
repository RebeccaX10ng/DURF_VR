using System.Collections;
using UnityEngine;

public class SequentialEnabler : MonoBehaviour
{
    public GameObject[] objectsToEnable; 
    private int currentIndex = 0;

    void Start()
    {
        StartCoroutine(EnableObjectsInSequence());
    }

    IEnumerator EnableObjectsInSequence()
    {
        while (true)
        {
            DisableAllObjects();
            
            if (objectsToEnable.Length > 0)
            {
                objectsToEnable[currentIndex].SetActive(true);
            }
            
            yield return new WaitForSeconds(3f);
            
            currentIndex = (currentIndex + 1) % objectsToEnable.Length;
        }
    }
    
    void DisableAllObjects()
    {
        foreach (GameObject obj in objectsToEnable)
        {
            obj.SetActive(false);
        }
    }
}