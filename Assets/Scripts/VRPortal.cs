using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VRPortal : MonoBehaviour
{
    //public Transform destination;
    public Vector3 offset; 
    public float cooldownTime = 3f;
    
    //禁用传送后不用的东西
    public GameObject[] objectsToDeactivate;
    public MonoBehaviour[] scriptsToDisable;

    private bool canTeleport = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && canTeleport)
        {
            Debug.Log("Player entered the portal");
            TeleportPlayer(other.transform);
            StartCoroutine(TeleportCooldown());
        }
    }

    private void TeleportPlayer(Transform player)
    {
        Vector3 targetPosition = player.position + offset;  
        player.position = targetPosition;
        //player.rotation = destination.rotation;
        
        foreach (GameObject obj in objectsToDeactivate)
        {
            if (obj != null)
            {
                obj.SetActive(false);  
            }
        }
        foreach (MonoBehaviour script in scriptsToDisable)
        {
            if (script != null)
            {
                script.enabled = false;  
            }
        }
    }

    private IEnumerator TeleportCooldown()
    {
        canTeleport = false;
        yield return new WaitForSeconds(cooldownTime);
        canTeleport = true;
    }
}