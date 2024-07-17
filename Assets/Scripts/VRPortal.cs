using UnityEngine;
using System.Collections;

public class VRPortal : MonoBehaviour
{
    public Transform destination;
    public float cooldownTime = 3f; 

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
        player.position = destination.position;
        player.rotation = destination.rotation;
    }

    private IEnumerator TeleportCooldown()
    {
        canTeleport = false;
        yield return new WaitForSeconds(cooldownTime);
        canTeleport = true;
    }
}