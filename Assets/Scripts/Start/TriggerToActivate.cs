using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerToActivate : MonoBehaviour
{
    public GameObject[] objectsToActivate;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player activates objects");
            ActivateObjects();
        }
    }// Update is called once per frame

    private void ActivateObjects()
    {
        foreach (GameObject obj in objectsToActivate)
        {
            obj.SetActive(true);
        }
    }
}
